using System.Collections.Generic;
using Umbraco.Web.Models;

namespace ThuanTanUmbraco.Models
{
    public class Product
    {
        public RenderModel RenderProduct { get; set; }
        public string Color { get; set; }
    }
    public class ProductColor
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public static class ListProductColors
    {
        static ListProductColors()
        {
            ProductColors = new List<ProductColor>
            {
                new ProductColor {
                    Key = "Almond",
                    Value = "(230,215,185,1)"
                },
                new ProductColor {
                    Key = "BrilliantSteel",
                    Value = "(242,242,242,1)"
                },
                new ProductColor {
                    Key = "ClayPink",
                    Value = "(240,213,202,1)"
                },
                new ProductColor {
                    Key = "DaisyYellow",
                    Value = "(244,222,0,1)"
                },
                new ProductColor {
                    Key = "MattBlack",
                    Value = "(22,22,22,1)"
                },
                new ProductColor {
                    Key = "MattSteelFingerprintProof",
                    Value = "(222,222,222,1)"
                },
                new ProductColor {
                    Key = "MetallicGrey",
                    Value = "(201,201,201,1)"
                },
                new ProductColor {
                    Key = "MetallicMint",
                    Value = "(153,173,168,1)"
                },
                new ProductColor {
                    Key = "MineralConcreteGrey",
                    Value = "(111,110,107,1)"
                },
                new ProductColor {
                    Key = "MineralGoldenBeach",
                    Value = "(203,187,162,1)"
                },
                new ProductColor {
                    Key = "MineralInfiniteGrey",
                    Value = "(64,64,64,1)"
                },
                new ProductColor {
                    Key = "MineralMustardYellow",
                    Value = "(208,154,62,1)"
                },
                new ProductColor {
                    Key = "MineralReflectiveBlue",
                    Value = "(62,93,99,1)"
                },
                new ProductColor {
                    Key = "MineralWindsorRed",
                    Value = "(108,56,61,1)"
                },
                new ProductColor {
                    Key = "MossGreen",
                    Value = "(97,131,89,1)"
                },
                new ProductColor {
                    Key = "PassionRed",
                    Value = "(215,42,42,1)"
                },
                new ProductColor {
                    Key = "Platinum",
                    Value = "(118,111,100,1)"
                },
                new ProductColor {
                    Key = "White",
                    Value = "(255,255,255,1)"
                }
            };
        }
        public static List<ProductColor> ProductColors { get; set; }
    }
}