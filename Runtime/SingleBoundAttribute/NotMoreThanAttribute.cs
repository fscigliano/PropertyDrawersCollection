namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     original version available in https://github.com/anchan828/property-drawer-collection
    /// </summary>
	public class NotMoreThanAttribute : SingleBoundAttribute
	{
	    public NotMoreThanAttribute(int upperBound) { IntBound = upperBound; }
	    public NotMoreThanAttribute(float upperBound) { FloatBound = upperBound; }
	}
}