namespace ErpConnector.Ax.DTO
{
    public class CategoryHierarchyDTO
    {
        private string _hierarchyModifier;
        public long AxRecId { get; set; }
        public string Name { get; set; }
        public object HierarchyModifier
        {
            get
            {
                if (string.IsNullOrEmpty(_hierarchyModifier) || _hierarchyModifier.ToLower() == "Category")
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            set
            {
                _hierarchyModifier = value.ToString();
            }
        }
    }
}
