namespace CandidateSystem
{
    public class CandidateProperty
    {
        /// <summary>
        /// ID
        /// </summary>
        public int PropertyID{ get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        public string PropertyName { get; set; }

        public CandidateProperty() { }

        public CandidateProperty(int propertyID, string propertyName)
        {
            PropertyID = propertyID;
            PropertyName = propertyName;
        }
    }
}
