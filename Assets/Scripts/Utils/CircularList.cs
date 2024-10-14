using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{

public class CircularList<T>
{
	public List<T> list;
	public int currPos;

	public CircularList(T[] elems)
	{
		list = new List<T>();

		foreach (T elem in elems)
		{
			list.Add(elem);
		}

		currPos = 0;
	}
	

	public T Current()
	{
		if (currPos < 0)
			currPos = list.Count - 1;
		else if (currPos >= list.Count)
			currPos = 0;
		return list[currPos];
	}


	/* Advance and wrap around if beyond list bound */
	public void Forward()
	{
		currPos = currPos < (list.Count) ? currPos + 1 : 0;
	}


	/* Ditto but move backward */
	public void Backward()
	{
		currPos = currPos > (0) ? currPos - 1 : list.Count - 1;
	}
	

	public void Add(T elem)
	{
		list.Add(elem);
	}


	public int Count() {return list.Count;}

}

}//Utils
