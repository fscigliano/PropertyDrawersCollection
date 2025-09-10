using UnityEngine;
using System;

namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     original version available in https://github.com/anchan828/property-drawer-collection
    /// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true)]
	public abstract class SingleBoundAttribute : PropertyAttributeBase
	{
	    public int IntBound
	    { 
	        get
	        {
	            if (FixedType != typeof(int))
	            {
	                throw new UnityException(FixedType.ToString() 
	                                         + " is set, asked for int in " + GetType());
	            }
	            return m_intBound;
	        }
	        protected set
	        {
	            FixedType = typeof(int);
	            m_intBound = value;
	        }
	    }

	    public float FloatBound
	    { 
	        get
	        {
	            if (FixedType != typeof(float))
	            {
	                throw new UnityException(FixedType.ToString() 
	                                         + " is set, asked for float in " + GetType());
	            }
	            return m_floatBound;
	        }
	        protected set
	        {
	            FixedType = typeof(float);
	            m_floatBound = value;
	        }
	    }

	    private Type FixedType { get; set; }
	    private float m_floatBound;
	    private int m_intBound;

	}
}