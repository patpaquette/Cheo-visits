using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace comp4004ProjDeliverable1.Model
{
    public class Patient
    {
        public event ACVFoundEventHandler ACVFound;

        private int _ID;
        private List<Visit> _visits;
        private bool _safe;
        private int _safeNessThreshold = 4;
        private List<Patient> _safetyPatientsBuffer;

        public Patient(int ID)
        {
            this.ID = ID;
            _visits = new List<Visit>();
            this._safetyPatientsBuffer = new List<Patient>();
        }

        public int ID
        {
            get
            {
                return this._ID;
            }

            set
            {
                this._ID = value;
            }
        }

        public List<Visit> Visits
        {
            get
            {
                if (this._visits.Count == 0)
                {
                    this._visits = DbMethods.getInstance().getPatientVisits(this.ID);
                }
                return this._visits;
            }
            set
            {
                this._visits = value;
            }
        }

        public string ShortString
        {
            get
            {
                return "Patient " + this._ID;
            }
        }

        public void SetSafetyThreshold(int safetyThreshold)
        {
            this._safeNessThreshold = safetyThreshold;
        }

        public bool IsStillSafe(List<Patient> patients, int acvSize, Visit newVisit)
        {
            if(this._safe == true)
                return this.IsSafe(patients, acvSize, newVisit);

            return false;
        }

        /*
        public bool IsSafe(int acvSize)
        {
            this._safe = 1;
            this.ACVFound += new ACVFoundEventHandler(this.safenessProcessFoundACVCallback);

            this.FindACVs(acvSize);

            if (this._safe != 0)
            {
                return true;
            }

            return false;
        }*/

        public bool IsSafe(List<Patient> patients, int acvSize, int safenessThreshold)
        {
            this._safeNessThreshold = safenessThreshold;
            return this.IsSafe(patients, acvSize, null);
        }

        public bool IsSafe(List<Patient> patients, int acvSize)
        {
            return this.IsSafe(patients, acvSize, null);
        }

        public bool IsSafe(List<Patient> patients, int acvSize, Visit mustInclude)
        {
            bool safe = true;
            this._safetyPatientsBuffer = new List<Patient>();
            Stopwatch sw = new Stopwatch();

            this.FindACVs(acvSize,
                (Patient sender, ACVFoundArgs args) =>
                {
                    sw.Start();
                    ACV acv = args.ACV;
                    int matchCount = 0;

                    if(safe){
                        foreach (Patient p in patients)
                        {
                            if (p.MatchesACV(acv) && p.ID != this.ID)
                            {
                                matchCount++;
                                this._safetyPatientsBuffer.Add(p);

                                if (matchCount >= this._safeNessThreshold)
                                {
                                    break;
                                }
                            }   
                        }
                    }
                    
                    if(matchCount < this._safeNessThreshold)
                    {
                        safe = false;
                    }
                    
                    sw.Stop();
                },
                mustInclude
            );


            if (safe)
            {
                this._safe = true;
            }
            else
            {
                this._safe = false;
            }

            return safe;
        }

        //checks if the patient has an ACV that matches the specified ACV
        public bool MatchesACV(ACV acv)
        {
            if (this._visits.Count == 0)
            {
                this._visits = DbMethods.getInstance().getPatientVisits(this.ID);
            }
            
            List<Visit> toMatch = new List<Visit>(this._visits);
            int indexToRemove = -1;
            int matchCount = 0;


            foreach (Visit v1 in acv.Visits)
            {
                for(int i = 0; i < toMatch.Count; i++)
                {
                    if (v1.equals(toMatch[i]))
                    {
                        indexToRemove = i;
                        matchCount++;
                        break;
                    }
                }

                if (indexToRemove >= 0)
                {
                    toMatch.RemoveAt(indexToRemove);
                    indexToRemove = -1;
                }
            }

            if (matchCount == acv.Visits.Count)
            {
                return true;
            }

            return false;
        }

        //get all ACVs that match the specified cm
        public List<ACV> GetCMMatches(ACV cm)
        {
            List<ACV> cmMatches = new List<ACV>();

            this.FindACVs(cm.Visits.Count, 
                (Patient sender, ACVFoundArgs args) =>
            {
                ACV acv = args.ACV;
                ACV tempCM = new ACV(cm.Visits);
                bool acvMatch = true;
                foreach (Visit v1 in acv.Visits)
                {
                    bool visitMatch = false;
                    Visit toRemove = null;
                    foreach (Visit v2 in tempCM.Visits)
                    {
                        if (v1.equals(v2))
                        {
                            toRemove = v2;
                            visitMatch = true;
                            break;
                        }
                    }

                    if (!visitMatch)
                    {
                        acvMatch = false;
                        break;
                    }
                    else
                    {
                        if (toRemove != null)
                        {
                            tempCM.Visits.Remove(toRemove);
                        }
                    }
                }

                if (acvMatch)
                {
                    cmMatches.Add(acv);
                }
            });

            return cmMatches;
        }

        //adds a visit to the patient
        public void AddVisit(Visit visit)
        {
            if (this.HasVisit(visit))
            {
                throw new Exception("This patient already has this visit");
            }

            this._visits.Add(visit);
            visit.Patient = this;
        }

        //check if the patient currently has a visit
        public bool HasVisit(Visit visit)
        {
            foreach (Visit v in this._visits)
            {
                if (v.ID == visit.ID)
                {
                    return true;
                }
            }

            return false;
        }

        //remove all visits
        public void ClearVisits()
        {
            this._visits.Clear();
        }

        public int GetVisitsNumber()
        {
            return this._visits.Count;
        }

        public Visit GetEarliestVisit()
        {
            Visit earliest = this._visits[0];

            foreach (Visit v in this._visits)
            {
                if (earliest.Date > v.Date)
                {
                    earliest = v;
                }
            }

            return earliest;
        }

        public Visit GetLatestVisit()
        {
            Visit latest = this._visits[0];

            foreach (Visit v in this._visits)
            {
                if (latest.Date < v.Date)
                {
                    latest = v;
                }
            }

            return latest;
        }

        //checks if this patients is similar to the specified patient
        public bool HasSimilarVisits(Patient p, float percentage)
        {
            List<Visit> pVisits = p.Visits;
            List<Visit> simVisits = new List<Visit>();
            int neededSimilarVisits = (int)Math.Ceiling(this.GetVisitsNumber() * percentage);
            int currentSimilarVisits = 0;

            foreach (Visit pv in pVisits)
            {
                foreach (Visit v in this._visits)
                {
                    if (v.equals(pv))
                    {
                        simVisits.Add(v);
                        currentSimilarVisits++;
                        break;
                    }

                }
            }

            bool similar = currentSimilarVisits >= neededSimilarVisits;
            if(similar){
                Console.WriteLine("Patient " + this.ID + " is similar to Patient " + p.ID + " because of the following visits(" + simVisits.Count + ")"); 
                foreach(Visit v in simVisits){
                    Console.Write("   " + v.ToString());
                }
            }

            return similar;
        }

        public bool HasNoSimilarVisits(Patient p)
        {
            List<Visit> pVisits = p.Visits;
            foreach (Visit pv in pVisits)
            {
                foreach (Visit v in this._visits)
                {
                    if (v.equals(pv))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string ToString()
        {
            return "Patient " + this._ID;
        }

        public void FindACVs(int maxSize, ACVFoundEventHandler callback)
        {
            this.FindACVs(maxSize, callback, null);
        }

        //public hook to search the tree
        /*public void FindACVs(int maxSize)
        {
            if (this.Visits.Count > 0)
            {
                ACVTreeNode root = new ACVTreeNode(null, null);
                List<Visit> currentACV = new List<Visit>();

                this.traverseACVTree(root, this.Visits, currentACV, 0, 0, maxSize, null);
            }
        }*/

        //public hook to search the tree and register a callback
        public void FindACVs(int maxSize, ACVFoundEventHandler callback, Visit mustInclude)
        {
            this.Visits = DbMethods.getInstance().getPatientVisits(this.ID);
            this.ACVFound = callback;
            if (this.Visits.Count > 0)
            {
                ACVTreeNode root = new ACVTreeNode(null, null);
                List<Visit> currentACV = new List<Visit>();

                this.traverseACVTree(root, this.Visits, currentACV, 0, 0, maxSize, mustInclude);
            }
        }

        //searches the ACV tree and triggers the ACVFound event when it reaches the size limit of the tree
        private void traverseACVTree(ACVTreeNode node, List<Visit> visits, List<Visit> currentACV, int index, int size, int maxSize, Visit isIncluded)
        {
            int count = 0;

            if(node.Visit != null)
                currentACV.Add(node.Visit);

            if (size == maxSize)
            {
                this.ACVFound(this, new ACVFoundArgs(new ACV(currentACV)));
                return;
            }

            for (int i = index; i < visits.Count; i++)
            {
               
                if (node.Visit == null && isIncluded != null && count < 1)
                {
                    if(isIncluded.equals(visits[i]))
                    {
                        node.addChild(new ACVTreeNode(visits[i], node));
                        count++;
                        break;
                    }
                }
                else
                {
                    node.addChild(new ACVTreeNode(visits[i], node));
                }
            }

            foreach (ACVTreeNode n in node.Children)
            {
                traverseACVTree(n, visits, new List<Visit>(currentACV), ++index, size + 1, maxSize, isIncluded);
            }
        }
    }

    public class ACVFoundArgs : EventArgs
    {
        private ACV _acv;

        public ACV ACV
        {
            get
            {
                return this._acv;
            }
        }

        public ACVFoundArgs(ACV acv)
        {
            this._acv = acv;
        }
    }

    public delegate void ACVFoundEventHandler(Patient sender, ACVFoundArgs args);
}
