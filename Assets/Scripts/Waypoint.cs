using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waypoints
{
    public class Waypoint : MonoBehaviour
    {
        public Waypoint[] adjacentWaypoints;
        static List<Waypoint> allWaypoints=new List<Waypoint>();

        void Awake()
        {
            allWaypoints.Add(this);
        }
        void OnDestroy()
        {
            allWaypoints = new List<Waypoint>();
        }
        public virtual void OnWaypointEnter()
        {

        }
        public virtual bool IsBlocked()
        {
            return false;
        }
        public static Path CreatePath(Waypoint start, Waypoint end)
        {
            Path p = new Path();
            p.Push(start);
            p = (CreatePathHelper(p, end));
            return p;
        }
        public void AddAdjacent(Waypoint w)
        {
            List<Waypoint> temp = new List<Waypoint>(adjacentWaypoints);
            if (temp.Contains(w) || w == this)
                return;
            temp.Add(w);
            adjacentWaypoints = temp.ToArray();
        }
        public static Path CreatePathHelper(Path prev, Waypoint end)
        {
            if (prev.End == end)
                return prev;
            List<Path> possiblePaths = new List<Path>();
            foreach (Waypoint w in prev.End.adjacentWaypoints)
            {
                Path p = null;
                if (prev.ContainsPoint(w))
                    continue;
                Path temp = new Path(prev);
                temp.Push(w);
                p = CreatePathHelper(temp, end);
                if (p != null)
                    possiblePaths.Add(p);
            }
            if (possiblePaths.Count == 0)
                return null;

            Path shortestPath = possiblePaths[0];
            foreach (Path path in possiblePaths)
            {
                if (path.Cost < shortestPath.Cost)
                    shortestPath = path;
            }
            return shortestPath;
        }
        public static Waypoint GetShorestPath(Waypoint w1, Waypoint w2, Waypoint end)
        {
            Path p1 = Waypoint.CreatePath(w1, end);
            Path p2 = Waypoint.CreatePath(w2, end);
            if (p1.Cost < p2.Cost)
                return w1;
            return w2;
        }

        public static Waypoint ClosestWaypointTo(Vector3 pos)
        {
            Waypoint closest = allWaypoints[0];
            float distance = 10000000;
            foreach (Waypoint item in allWaypoints)
            {
                float d = Vector3.Distance(pos, item.transform.position);
                if (d < distance)
                {
                    distance = d;
                    closest = item;
                }
            }
            return closest;
        }
    }
}
namespace Waypoints
{
    public class Path
    {
        protected List<Waypoint> waypoints;
        public Waypoint End
        {
            get
            {
                if (IsEmpty())
                    return null;
                return waypoints[waypoints.Count - 1];
            }
        }
        public int Size
        {
            get
            {
                return waypoints.Count;
            }
        }

        public float Cost
        {
            get
            {
                if (waypoints.Count < 2)
                    return 0;
                float result = 0;
                for (int i = 0; i < waypoints.Count - 1; i++)
                {
                    result += Mathf.Abs(waypoints[i].transform.position.x - waypoints[i + 1].transform.position.x);
                }
                return result;
            }
        }
        public Path()
        {
            waypoints = new List<Waypoint>();
        }

        public Path(List<Waypoint> path)
        {
            waypoints = new List<Waypoint>();
            foreach (Waypoint w in path)
                waypoints.Add(w);
        }

        public Path(Path p)
        {
            waypoints = new List<Waypoint>();
            foreach (Waypoint w in p.waypoints)
                waypoints.Add(w);
        }

        public bool IsEmpty()
        {
            return waypoints.Count < 1;
        }
        public void Push(Waypoint w)
        {
            waypoints.Add(w);
        }
        public Waypoint Peek()
        {
            if (!IsEmpty())
                return waypoints[0];
            return null;
        }
        public Waypoint Pop()
        {
            if (IsEmpty())
                return null;
            Waypoint w = Peek();
            waypoints.RemoveAt(0);
            return w;
        }
        public bool ContainsPoint(Waypoint w)
        {
            return waypoints.Contains(w);
        }
        public override string ToString()
        {
            string output = "";
            foreach (Waypoint w in waypoints)
            {
                output += " -> " + w.name;
            }
            return output;
        }
    }
}
