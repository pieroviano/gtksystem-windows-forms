namespace System.Windows.Forms
{
	public class ColumnStyle : TableLayoutStyle
	{
        private float width;

        public float Width
        {
            get => width;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(width));
                }
                width = value;
            }
        }

        public ColumnStyle()
		{
			
		}

		public ColumnStyle(SizeType sizeType)
		{
            this.SizeType = sizeType;
		}

		public ColumnStyle(SizeType sizeType, float width)
		{
            this.SizeType = sizeType;
            if (width < 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }
			this.Width = width;
        }
	}
}
