

using HalconDotNet;

public partial class HDevelopExport
{

  // Main procedure 
  public void Action(HObject image ,HTuple window)
  {

        HObject ho_Image12, ho_GrayImage, ho_Edges;
        HObject ho_SelectedContours, ho_UnionContours, ho_SelectedContours1;
        HObject ho_ContoursUnion, ho_SelectedContours2, ho_Cross;
        HTuple hv_Area = new HTuple(), hv_Row = new HTuple();
        HTuple hv_Column = new HTuple(), hv_PointOrder = new HTuple();
        HOperatorSet.GenEmptyObj(out ho_Image12);
        HOperatorSet.GenEmptyObj(out ho_GrayImage);
        HOperatorSet.GenEmptyObj(out ho_Edges);
        HOperatorSet.GenEmptyObj(out ho_SelectedContours);
        HOperatorSet.GenEmptyObj(out ho_UnionContours);
        HOperatorSet.GenEmptyObj(out ho_SelectedContours1);
        HOperatorSet.GenEmptyObj(out ho_ContoursUnion);
        HOperatorSet.GenEmptyObj(out ho_SelectedContours2);
        HOperatorSet.GenEmptyObj(out ho_Cross);
        ho_Image12.Dispose();
        ho_Image12 = image;
        HOperatorSet.SetColor(window, "green");
        HOperatorSet.SetLineWidth(window, 4);
        ho_GrayImage.Dispose();
        HOperatorSet.Rgb1ToGray(ho_Image12, out ho_GrayImage);

        ho_Edges.Dispose();
        HOperatorSet.EdgesSubPix(ho_GrayImage, out ho_Edges, "canny", 1, 20, 40);

        ho_SelectedContours.Dispose();
        HOperatorSet.SelectContoursXld(ho_Edges, out ho_SelectedContours, "contour_length",
            30, 90000, -0.5, 0.5);

        ho_UnionContours.Dispose();
        HOperatorSet.UnionAdjacentContoursXld(ho_SelectedContours, out ho_UnionContours,
            10, 1, "attr_keep");

        ho_SelectedContours1.Dispose();
        HOperatorSet.SelectContoursXld(ho_UnionContours, out ho_SelectedContours1, "closed",
            0.5, 200, -0.5, 0.5);

        ho_ContoursUnion.Dispose();
        HOperatorSet.Union2ClosedContoursXld(ho_SelectedContours1, ho_SelectedContours1,
            out ho_ContoursUnion);

        ho_SelectedContours2.Dispose();
        HOperatorSet.SelectContoursXld(ho_ContoursUnion, out ho_SelectedContours2, "contour_length",
            0.5, 500, -0.5, 0.5);
        hv_Area.Dispose(); hv_Row.Dispose(); hv_Column.Dispose(); hv_PointOrder.Dispose();
        HOperatorSet.AreaCenterXld(ho_SelectedContours2, out hv_Area, out hv_Row, out hv_Column,
            out hv_PointOrder);

        ho_Cross.Dispose();
        HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Row, hv_Column, 16, 0.785398);

        HOperatorSet.ClearWindow(window);


        HOperatorSet.DispObj(ho_Image12, window);


        HOperatorSet.DispObj(ho_ContoursUnion, window);


        HOperatorSet.SetColor(window, "red");


        HOperatorSet.DispObj(ho_Cross, window);


        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
            HOperatorSet.DispText(window, ((("x=" + hv_Column) + "\n") + "y=") + hv_Row,
                "image", hv_Row, hv_Column, "black", new HTuple(), new HTuple());
        }




        ho_Image12.Dispose();
        ho_GrayImage.Dispose();
        ho_Edges.Dispose();
        ho_SelectedContours.Dispose();
        ho_UnionContours.Dispose();
        ho_SelectedContours1.Dispose();
        ho_ContoursUnion.Dispose();
        ho_SelectedContours2.Dispose();
        ho_Cross.Dispose();

        hv_Area.Dispose();
        hv_Row.Dispose();
        hv_Column.Dispose();
        hv_PointOrder.Dispose();

    }




}

