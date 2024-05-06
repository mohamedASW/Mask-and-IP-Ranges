using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace MASK_IP_RANGE
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string strRegex = "^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)" +
                              "\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

            Regex re = new Regex(strRegex);
            string path = @"C:\Users\ma247\OneDrive\Desktop\";
            Console.Write("Enter Number Of Devices That You Want In Your Network  : ");
            int DNumber = 0;
        Again: if (!int.TryParse(Console.ReadLine(), out DNumber))
            {
                Console.Write("plaease Check Your Value ...!\nEnter Again Plz: ");

                goto Again;
            }
        Again2: Console.Write("Enter A Random IP,It Is Prefer To Be On Form(10.____.____.____) | (172.16.____.____) | (192.168.____.____) : ");
            string IpReciever = Console.ReadLine();
            if (re.IsMatch(IpReciever))
            {
                IpAddressing.PostDevices_Ip(DNumber, IpReciever);
                var b = IpAddressing.GetMaskBinary();
                var d = IpAddressing.GetMaskDecimal();
                var h = IpAddressing.GetHOfDevices();
                var sub = IpAddressing.GetSubnetDecimal();
                var sub2 =IpAddressing.GetSubnetBinary();
                var Broad = IpAddressing.GetBroadCast();
                var Broad2 = IpAddressing.GetBroadCastBinary();
                var Blocksize = IpAddressing.GetBlockSize();
                var SubnetNumbers = IpAddressing.GetSubnetsNumber();
                Console.WriteLine($"H : {h}" +
                    $"\nMask As Decimal : {d}" +
                    $"\nMask As Binary : {b}" +
                    $"\nOne subnet As Decimal : {sub}" +
                    $"\nOne subnet As Binary : {sub2}" +
                    $"\nOne Broad cast As Decimal : {Broad}" +
                    $"\nOne Broad Cast as Binary: {Broad2}" +
                    $"\nBlock Size : {Blocksize}" +
                    $"\nSubnetNumbers : {SubnetNumbers}");
            using (StreamWriter sw = new StreamWriter(path+"Mask_Subnet_Broad.txt",true))           
            {
                sw.WriteLine($"H : {h}" +
                    $"\nMask As Decimal : {d}" +
                    $"\nMask As Binary : {b}" +
                    $"\nOne subnet As Decimal : {sub}" +
                    $"\nOne subnet As Binary : {sub2}" +
                    $"\nOne Broad cast As Decimal : {Broad}" +
                    $"\nOne Broad Cast as Binary: {Broad2}" +
                    $"\nBlock Size : {Blocksize}" +
                    $"\nSubnetNumbers : {SubnetNumbers}"+
                    $"\n\n--------------------------------------\n\n");
                    sw.Close();
            }
            }
            else
                goto Again2;



        }
    }
}