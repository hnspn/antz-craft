using UnityEngine;

public class Point
{
	public int X, Y;
	
	public Point(int px, int py)
	{
		X = px;
		Y = py;
	}

    public override string ToString() {
        return "x:" + X + "| y:" + Y;
    }
}
