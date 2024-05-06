using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASK_IP_RANGE
{
    ///<include file='Genrate.xml' path='doc/members/member[@name="IP"]/*'/>
    internal static class IpAddressing
    {
        private static string _maskBinary= string.Empty;
        private static double Hnum;
        private static string MDecimal= string .Empty;
        private static string _SubnetD= string .Empty;
        private static string _SubnetB= string .Empty;
        private static string _ip= string.Empty;
        private static string _ipB= string.Empty;
        private static string _broadCast= string.Empty;
        private static string _broadCastB= string.Empty;
        private static double _blockSize ;
        private static double _subnetsNumber;
        ///<include file='Genrate.xml' path='doc/members/member[@name="PostDevices_Ip"]/*'/>
        public static void PostDevices_Ip(double DNum,string Ip)
            {
                 Hnum = Math.Ceiling(Math.Log2(DNum));
                _ip = Ip;
                MBinaryConveter();
                MDecimalConverter();
                SubnetDConverter();
                SubnetBConverter();
               BroadCastConverter();
               blocksizeConverter();
            }

            static private void MBinaryConveter()
            {
                for (int i = 1; i <=32-Hnum; i++)
                {

                    _maskBinary += '1';
                    _maskBinary += (i % 8 == 0) ?  '.' : "";
                }
                for ( int i = (int)((32 - Hnum)+1); i <= 32; i++)
                {
                    _maskBinary += '0';
                    _maskBinary += ((i % 8 == 0)&& (i != 32)) ? '.' : "";
                }
            }
            static private void MDecimalConverter()
            {
                var octs = _maskBinary.Split('.');
                foreach (var oct in octs)
                {
                    MDecimal += $"{Convert.ToInt32(oct, 2)}.";
                }
                MDecimal = MDecimal.Substring(0, MDecimal.Length-1);
            }
            static private void SubnetDConverter()
            {
             var maskocts = MDecimal.Split(".");
             var ipocts = _ip.Split(".");
              for (int i = 0;i< maskocts.Length; i++)
               {
                 var oct = (int.Parse(maskocts[i]) & int.Parse(ipocts[i]));
                _SubnetD += $"{oct}.";
               }
            _SubnetD = _SubnetD.Substring(0, _SubnetD.Length - 1);
            }
            static private void SubnetBConverter()
            {
             var octs = _SubnetD.Split(".");
                foreach (var oct in octs)
                {
                _SubnetB += $"{Convert.ToString(int.Parse(oct), 2).PadLeft(8,'0')}.";
                }
              _SubnetB = _SubnetB.Substring(0, _SubnetB.Length - 1);
            }

            static private void BroadCastConverter()
            {
                getIpBinary();
                for (int i = 0; i < _maskBinary.Length; i++)
                {
                    if (_maskBinary[i] == '1' || _maskBinary[i] == '.')

                    _broadCastB += _ipB[i];
                   
                    else
                    _broadCastB += '1';

                }
              var octs = _broadCastB.Split('.');
                 
                foreach (var oct in octs)
                {
                _broadCast +=$"{ Convert.ToInt32(oct, 2)}.";
                }
            _broadCast = _broadCast.Substring(0, _broadCast.Length - 1);

            static  void getIpBinary()
            {
             var octs = _ip.Split(".");
              foreach (var oct in octs)   
                {
                _ipB += $"{Convert.ToString(int.Parse(oct), 2).PadLeft(8,'0')}.";
                }
              _ipB = _ipB.Substring(0,_ipB.Length - 1);
            }
        }
             static   private  void blocksizeConverter() 
            {
              var octs = _maskBinary.Split(".");
                for (int i = 0; i < octs.Length; i++)
                {
                   var oct = Convert.ToInt32(octs[i], 2);
                    if (oct !=255 && oct !=0)
                    {
                          int counter = 0;
                           foreach (var bit in octs[i])
                           {
                              if(bit!='0')counter++;
                           }
                    _blockSize = Math.Pow(2, 8 - counter) * 1;
                    _subnetsNumber = Math.Pow(2, counter);

                    }
                }
            }
            
        
           public static string getIpBinary() => _ipB;  
           public static string GetBroadCast() => _broadCast;
           public static string GetBroadCastBinary() => _broadCastB;
           public static double GetHOfDevices() => Hnum;
           public static string GetMaskBinary()=> _maskBinary;
           public static string GetMaskDecimal() => MDecimal;
           public static string GetSubnetDecimal() => _SubnetD;
           public static string GetSubnetBinary() => _SubnetB;
           public static double GetBlockSize() => _blockSize;
           public static double GetSubnetsNumber() => _subnetsNumber;
            
           

    }
}
