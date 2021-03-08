using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.UiComponents
{
    [AddComponentMenu("Layout/Flexible Grid Layout Group")]
    public class FlexibleGridLayoutGroup : LayoutGroup
    {
        public enum Corner
        {
            UpperLeft = 0,
            UpperRight = 1,
            UpperCenter = 2,
            LowerLeft = 3,
            LowerRight = 4,
            LowerCenter = 5
        }

        public enum Axis
        {
            Horizontal = 0,
            Vertical = 1
        }

        public enum Constraint
        {
            Flexible = 0,
            FixedColumnCount = 1,
            FixedRowCount = 2
        }

        [SerializeField] protected Corner m_StartCorner = Corner.UpperLeft;

        public Corner startCorner
        {
            get => m_StartCorner;
            set => SetProperty(ref m_StartCorner, value);
        }

        [SerializeField] protected Axis m_StartAxis = Axis.Horizontal;

        public Axis startAxis
        {
            get => m_StartAxis;
            set => SetProperty(ref m_StartAxis, value);
        }

        [SerializeField] protected Vector2 m_CellSize = new Vector2(100, 100);

        public Vector2 cellSize
        {
            get => m_CellSize;
            set => SetProperty(ref m_CellSize, value);
        }

        [SerializeField] protected bool m_IsFlexibleCellWidth = false;

        public bool IsFlexibleCellWidth
        {
            get => m_IsFlexibleCellWidth;
            set => SetProperty(ref m_IsFlexibleCellWidth, value);
        }

        [SerializeField] protected bool m_IsFlexibleCellHeight = false;

        public bool IsFlexibleCellHeight
        {
            get => m_IsFlexibleCellHeight;
            set => SetProperty(ref m_IsFlexibleCellHeight, value);
        }

        [SerializeField] protected Vector2 m_Spacing = Vector2.zero;

        public Vector2 spacing
        {
            get => m_Spacing;
            set => SetProperty(ref m_Spacing, value);
        }

        [SerializeField] protected Constraint m_Constraint = Constraint.Flexible;

        public Constraint constraint
        {
            get => m_Constraint;
            set => SetProperty(ref m_Constraint, value);
        }

        [SerializeField] protected int m_ConstraintCount = 2;

        public int constraintCount
        {
            get => m_ConstraintCount;
            set => SetProperty(ref m_ConstraintCount, Mathf.Max(1, value));
        }

        [SerializeField] protected bool StretchX = false;
        [SerializeField] protected bool StretchY = false;

        [SerializeField] protected bool BorderSpacing = false;

        public int ColumnsCount => (int) GetCellCount().x;

        public int RowsCount => (int) GetCellCount().y;

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();
            constraintCount = constraintCount;
        }

#endif

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            int minCount = 0;
            int preferredCount = 0;
            switch (m_Constraint)
            {
                case Constraint.FixedColumnCount:
                    minCount = preferredCount = m_ConstraintCount;
                    break;
                case Constraint.FixedRowCount:
                    minCount = preferredCount = Mathf.CeilToInt(rectChildren.Count / (float) m_ConstraintCount - 0.001f);
                    break;
                default:
                    minCount = 1;
                    preferredCount = Mathf.CeilToInt(Mathf.Sqrt(rectChildren.Count));
                    break;
            }

            if (IsFlexibleCellWidth)
            {
                float width = rectTransform.rect.size.x;
                int columnCount = ColumnsCount;
                float freeWidth = width - (padding.horizontal + spacing.x * (columnCount - 1) + 0.001f);
                cellSize = new Vector2(freeWidth / columnCount, cellSize.y);
            }

            if (IsFlexibleCellHeight)
            {
                float height = rectTransform.rect.size.y;
                int rowCount = RowsCount;
                float freeHeight = height - (padding.vertical + spacing.y * (rowCount - 1) + 0.001f);
                cellSize = new Vector2(cellSize.x, freeHeight / rowCount);
            }

            SetLayoutInputForAxis(padding.horizontal + (cellSize.x + spacing.x) * minCount - spacing.x, padding.horizontal + (cellSize.x + spacing.x) * preferredCount - spacing.x, -1, 0);
        }

        public override void CalculateLayoutInputVertical()
        {
            int rowsCount = 0;
            switch (m_Constraint)
            {
                case Constraint.FixedColumnCount:
                    rowsCount = Mathf.CeilToInt(rectChildren.Count / (float) m_ConstraintCount - 0.001f);
                    break;
                case Constraint.FixedRowCount:
                    rowsCount = m_ConstraintCount;
                    break;
                default:
                {
                    float width = rectTransform.rect.size.x;
                    int cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - padding.horizontal + spacing.x + 0.001f) / (cellSize.x + spacing.x)));
                    rowsCount = Mathf.CeilToInt(rectChildren.Count / (float) cellCountX);
                    break;
                }
            }

            float minSpace = padding.vertical + (cellSize.y + spacing.y) * rowsCount - spacing.y;
            SetLayoutInputForAxis(minSpace, minSpace, -1, 1);
        }

        public override void SetLayoutHorizontal()
        {
            SetCellsAlongAxis(0);
        }

        public override void SetLayoutVertical()
        {
            SetCellsAlongAxis(1);
        }

        private Vector2 GetCellCount()
        {
            Vector2 spacing_tmp = spacing;
            int cellCountX = 1;
            int cellCountY = 1;
            float width = rectTransform.rect.size.x;
            float height = rectTransform.rect.size.y;

            if (m_Constraint == Constraint.FixedColumnCount)
            {
                cellCountX = m_ConstraintCount;
                cellCountY = Mathf.CeilToInt(rectChildren.Count / (float) cellCountX - 0.001f);
            }
            else if (m_Constraint == Constraint.FixedRowCount)
            {
                cellCountY = m_ConstraintCount;
                cellCountX = Mathf.CeilToInt(rectChildren.Count / (float) cellCountY - 0.001f);
            }
            else
            {
                if (cellSize.x + spacing_tmp.x <= 0)
                    cellCountX = int.MaxValue;
                else
                    cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - padding.horizontal + spacing_tmp.x + 0.001f) / (cellSize.x + spacing_tmp.x)));

                if (cellSize.y + spacing_tmp.y <= 0)
                    cellCountY = int.MaxValue;
                else
                    cellCountY = Mathf.Max(1, Mathf.FloorToInt((height - padding.vertical + spacing_tmp.y + 0.001f) / (cellSize.y + spacing_tmp.y)));
            }

            return new Vector2(cellCountX, cellCountY);
        }

        private void SetCellsAlongAxis(int axis)
        {
            Vector2 spacing_tmp = spacing;
            //     if (StretchX) spacing_tmp.x = 0;
            //     if (StretchY) spacing_tmp.y = 0;

            // Normally a Layout Controller should only set horizontal values when invoked for the horizontal axis
            // and only vertical values when invoked for the vertical axis.
            // However, in this case we set both the horizontal and vertical position when invoked for the vertical axis.
            // Since we only set the horizontal position and not the size, it shouldn't affect children's layout,
            // and thus shouldn't break the rule that all horizontal layout must be calculated before all vertical layout.

            if (axis == 0)
            {
                // Only set the sizes when invoked for horizontal axis, not the positions.
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    RectTransform rect = rectChildren[i];

                    m_Tracker.Add(this, rect, DrivenTransformProperties.Anchors | DrivenTransformProperties.AnchoredPosition | DrivenTransformProperties.SizeDelta);

                    rect.anchorMin = Vector2.up;
                    rect.anchorMax = Vector2.up;
                    rect.sizeDelta = cellSize;
                }

                return;
            }

            float width = rectTransform.rect.size.x;
            float height = rectTransform.rect.size.y;

            float width_tmp = rectTransform.rect.size.x - padding.horizontal;
            float height_tmp = rectTransform.rect.size.y - padding.vertical;

            int cellCountX = 1;
            int cellCountY = 1;
            if (m_Constraint == Constraint.FixedColumnCount)
            {
                cellCountX = m_ConstraintCount;
                cellCountY = Mathf.CeilToInt(rectChildren.Count / (float) cellCountX - 0.001f);
            }
            else if (m_Constraint == Constraint.FixedRowCount)
            {
                cellCountY = m_ConstraintCount;
                cellCountX = Mathf.CeilToInt(rectChildren.Count / (float) cellCountY - 0.001f);
            }
            else
            {
                if (cellSize.x + spacing_tmp.x <= 0)
                    cellCountX = int.MaxValue;
                else
                    cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - padding.horizontal + spacing_tmp.x + 0.001f) / (cellSize.x + spacing_tmp.x)));

                if (cellSize.y + spacing_tmp.y <= 0)
                    cellCountY = int.MaxValue;
                else
                    cellCountY = Mathf.Max(1, Mathf.FloorToInt((height - padding.vertical + spacing_tmp.y + 0.001f) / (cellSize.y + spacing_tmp.y)));
            }

            int cornerX = 0;
            int cornerY = 1;

            switch ((int) startCorner)
            {
                case 0:
                    cornerX = 0;
                    cornerY = 0;
                    break; //upper left
                case 1:
                    cornerX = 1;
                    cornerY = 0;
                    break; //upper right
                case 2:
                    cornerX = 3;
                    cornerY = 0;
                    break; //upper center
                case 3:
                    cornerX = 0;
                    cornerY = 1;
                    break; //lower left
                case 4:
                    cornerX = 1;
                    cornerY = 1;
                    break; //lower right
                case 5:
                    cornerX = 3;
                    cornerY = 1;
                    break; //lower center
            }

            int cellsPerMainAxis, actualCellCountX, actualCellCountY;
            if (startAxis == Axis.Horizontal)
            {
                cellsPerMainAxis = cellCountX;
                actualCellCountX = Mathf.Clamp(cellCountX, 1, rectChildren.Count);
                actualCellCountY = Mathf.Clamp(cellCountY, 1, Mathf.CeilToInt(rectChildren.Count / (float) cellsPerMainAxis));
            }
            else
            {
                cellsPerMainAxis = cellCountY;
                actualCellCountY = Mathf.Clamp(cellCountY, 1, rectChildren.Count);
                actualCellCountX = Mathf.Clamp(cellCountX, 1, Mathf.CeilToInt(rectChildren.Count / (float) cellsPerMainAxis));
            }

            Vector2 requiredSpace = new Vector2(actualCellCountX * cellSize.x + (actualCellCountX - 1) * spacing_tmp.x, actualCellCountY * cellSize.y + (actualCellCountY - 1) * spacing_tmp.y);
            Vector2 startOffset = new Vector2(GetStartOffset(0, requiredSpace.x), GetStartOffset(1, requiredSpace.y));

            Vector2 offset = startOffset;

            if (StretchY)
            {
                spacing_tmp.y = (height_tmp - cellSize[1] * actualCellCountY) / Mathf.Max(1, actualCellCountY + (BorderSpacing ? -1 : 1));
                offset.y = padding.top + (BorderSpacing ? 0 : spacing_tmp.y);
            }

            for (int i = 0; i < rectChildren.Count; i++)
            {
                float positionX;
                float positionY;
                if (startAxis == Axis.Horizontal)
                {
                    positionX = i % cellsPerMainAxis;
                    positionY = i / cellsPerMainAxis;
                }
                else
                {
                    positionX = i / cellsPerMainAxis;
                    positionY = i % cellsPerMainAxis;
                }

                if (cornerX == 1) positionX = actualCellCountX - 1 - positionX;
                if (cornerY == 1) positionY = actualCellCountY - 1 - positionY;

                if (StretchX && (rectChildren.Count - i >= rectChildren.Count % actualCellCountX))
                {
                    spacing_tmp.x = (width_tmp - cellSize[0] * actualCellCountX) / Mathf.Max(1, actualCellCountX + (BorderSpacing ? -1 : 1));
                }

                if (StretchX && i == 0)
                {
                    offset.x = padding.left + (BorderSpacing ? 0 : spacing_tmp.x);
                }

                if (startAxis == Axis.Horizontal)
                {
                    if (cornerX == 3 && rectChildren.Count % actualCellCountX != 0)
                    {
                        if (rectChildren.Count - i <= rectChildren.Count % actualCellCountX)
                        {
                            offset.x = padding.left - (!BorderSpacing && !StretchX ? spacing_tmp.x : 0) + (BorderSpacing ? 0 : spacing_tmp.x) + (!StretchX ? startOffset.x : 0) + (cellSize[0] + spacing_tmp[0]) * (actualCellCountX - rectChildren.Count % actualCellCountX) * 0.5f;
                        }
                    }
                }

                SetChildAlongAxis(rectChildren[i], 0, offset.x + (cellSize[0] + spacing_tmp[0]) * positionX, cellSize[0]);
                SetChildAlongAxis(rectChildren[i], 1, offset.y + (cellSize[1] + spacing_tmp[1]) * positionY, cellSize[1]);
            }
        }
    }
}