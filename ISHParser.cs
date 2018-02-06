
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *
 *  Produce simplified form from ISH file.
 * 
 *       Parameters:
 *           1st = Input File Name
 *           2nd = Output File Name
 *           3rd = Logging level
 *           4th = Logging Filter #1
 *           5th = Logging Filter #2
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Date:       Developer  PR/CR #   Description of changes
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * 03/10/2011  ras        ????      Created.
 * 
 * 06/06/2012  ras        ????      Added AW1-AW4 (Automated Present Weather)
 *                                  Corrected problem when OC1 was missing
 * 
 * 06/21/2012  ras        ????      Modified Wind Dir logic to set value to 990 when
 *                                  Type code is 'V'
 *                                  Added MW4 (Manual Present Weather)
 * 02/14/2012  msm336@cornell.edu   forked from ftp://ftp.ncdc.noaa.gov/pub/data/noaa/ishJava.java                             
 * 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.IO;
using System.Text;

namespace MLPrognoza.Data
{
    public class ISHParser
    {
        static string sProgramName = "ishJava.java";
        static string sDebugName = "ishJava";
        static string sInFileName = "";
        static string sOutFileName = "";
        static BinaryWriter fDebug = null;

        static bool bVerbose = false;
        static bool bOnce = false;
        static bool bStdErr = false;

        static string sOutputRecord = "";
        static string sControlSection = "";
        static string sMandatorySection = "";
        static int iCounter = 0;
        static int iLength = 0;
        static int iOffset = 25;
        static int iObsKey = 0;
        static int iWork = 0;
        static string[] sWW1234;
        static string[] sAW1234;
        static float fWork = 0.0f;
        static float fWorkSave = 0.0f;
        static string sConcat = "";
        static string sConcatDate = "";
        static string sConcatMonth = "";
        static string sMessage = "";

        static int iPROD = 0;                   // Only print basic run info
        static int iDEBUG = 1;                   // Print lots of info, such as debug messages
        static int iLogLevel = 0;                   // Default value for this run.
        static string p_sFilter1 = "None";
        static string p_sFilter2 = "None";

        static string fmt03 = "000";         // 3-char int   (keep leading zeros)
        static string fmt4_1 = "#0.0";        // 4-char float
        static string fmt6_1 = "###0.0";      // 6-char float
        static string fmt5_2 = "##0.00";      // 5-char float
        static string fmt02 = "#0";          // 2-char int

        //  Fields making up the Control Data Section.
        static string sCDS = "";
        static string sCDS_Fill1 = "";
        static string sCDS_ID = "";
        static string sCDS_Wban = "";
        static string sCDS_Year = "";
        static string sCDS_Month = "";
        static string sCDS_Day = "";
        static string sCDS_Hour = "";
        static string sCDS_Minute = "";
        static string sCDS_Fill2 = "";

        //  Fields making up the Mandatory Data Section.
        static string sMDS = "";
        static string sMDS_Dir = "";
        static string sMDS_DirQ = "";
        static string sMDS_DirType = "";
        static string sMDS_Spd = "";
        static string sMDS_Fill2 = "";
        static string sMDS_Clg = "";
        static string sMDS_Fill3 = "";
        static string sMDS_Vsb = "";
        static string sMDS_Fill4 = "";
        static string sMDS_TempSign = "";
        static string sMDS_Temp = "";
        static string sMDS_Fill5 = "";
        static string sMDS_DewpSign = "";
        static string sMDS_Dewp = "";
        static string sMDS_Fill6 = "";
        static string sMDS_Slp = "";
        static string sMDS_Fill7 = "";

        //  REM offset
        static int iREM_IndexOf = 0;

        //  Fields making up the OC1 element
        //    Sample Element=[OC1...]
        static int iOC1_IndexOf = 0;
        static int iOC1_Length = 8;
        static string sOC1 = "";
        static string sOC1_Fill1 = "";
        static string sOC1_Gus = "";
        static string sOC1_Fill2 = "";

        //  Fields making up the GF1 element
        static int iGF1_IndexOf = 0;
        static int iGF1_Length = 26;
        static string sGF1 = "";
        static string sGF1_Fill1 = "";
        static string sGF1_Skc = "";
        static string sGF1_Fill2 = "";
        static string sGF1_Low = "";
        static string sGF1_Fill3 = "";
        static string sGF1_Med = "";
        static string sGF1_Fill4 = "";
        static string sGF1_Hi = "";
        static string sGF1_Fill5 = "";

        //    static int    iMW_Counter   = 0;
        //  Fields making up the MW1-7 elements
        static int iMW1_IndexOf = 0;
        static int iMW1_Length = 6;
        static string sMW1 = "";
        static string sMW1_Fill1 = "";
        static string sMW1_Ww = "";
        static string sMW1_Fill2 = "";

        static int iMW2_IndexOf = 0;
        static int iMW2_Length = 6;
        static string sMW2 = "";
        static string sMW2_Fill1 = "";
        static string sMW2_Ww = "";
        static string sMW2_Fill2 = "";

        static int iMW3_IndexOf = 0;
        static int iMW3_Length = 6;
        static string sMW3 = "";
        static string sMW3_Fill1 = "";
        static string sMW3_Ww = "";
        static string sMW3_Fill2 = "";

        static int iMW4_IndexOf = 0;
        static int iMW4_Length = 6;
        static string sMW4 = "";
        static string sMW4_Fill1 = "";
        static string sMW4_Ww = "";
        static string sMW4_Fill2 = "";

        //  Fields making up the AY1 element
        static int iAY1_IndexOf = 0;
        static int iAY1_Length = 8;
        static string sAY1 = "";
        static string sAY1_Fill1 = "";
        static string sAY1_Pw = "";
        static string sAY1_Fill2 = "";

        //  Fields making up the MA1 element
        static int iMA1_IndexOf = 0;
        static int iMA1_Length = 15;
        static string sMA1 = "";
        static string sMA1_Fill1 = "";
        static string sMA1_Alt = "";
        static string sMA1_Fill2 = "";
        static string sMA1_Stp = "";
        static string sMA1_Fill3 = "";

        // Max/Min fields
        static string sMaxTemp = "";
        static string sMinTemp = "";

        //  Fields making up the KA1 element
        static int iKA1_IndexOf = 0;
        static int iKA1_Length = 13;
        static string sKA1 = "";
        static string sKA1_Fill1 = "";
        static string sKA1_Code = "";
        static string sKA1_Temp = "";
        static string sKA1_Fill2 = "";

        //  Fields making up the KA2 element
        static int iKA2_IndexOf = 0;
        static int iKA2_Length = 13;
        static string sKA2 = "";
        static string sKA2_Fill1 = "";
        static string sKA2_Code = "";
        static string sKA2_Temp = "";
        static string sKA2_Fill2 = "";

        // Precip fields
        static string sPcp01 = "";
        static string sPcp01t = "";
        static string sPcp06 = "";
        static string sPcp06t = "";
        static string sPcp24 = "";
        static string sPcp24t = "";
        static string sPcp12 = "";
        static string sPcp12t = "";

        //  Fields making up the AA1 element
        static int iAA1_IndexOf = 0;
        static int iAA1_Length = 11;
        static string sAA1 = "";
        static string sAA1_Fill1 = "";
        static string sAA1_Hours = "";
        static string sAA1_Pcp = "";
        static string sAA1_Trace = "";
        static string sAA1_Fill2 = "";

        //  Fields making up the AA2 element
        static int iAA2_IndexOf = 0;
        static int iAA2_Length = 11;
        static string sAA2 = "";
        static string sAA2_Fill1 = "";
        static string sAA2_Hours = "";
        static string sAA2_Pcp = "";
        static string sAA2_Trace = "";
        static string sAA2_Fill2 = "";

        //  Fields making up the AA3 element
        static int iAA3_IndexOf = 0;
        static int iAA3_Length = 11;
        static string sAA3 = "";
        static string sAA3_Fill1 = "";
        static string sAA3_Hours = "";
        static string sAA3_Pcp = "";
        static string sAA3_Trace = "";
        static string sAA3_Fill2 = "";

        //  Fields making up the AA4 element
        static int iAA4_IndexOf = 0;
        static int iAA4_Length = 11;
        static string sAA4 = "";
        static string sAA4_Fill1 = "";
        static string sAA4_Hours = "";
        static string sAA4_Pcp = "";
        static string sAA4_Trace = "";
        static string sAA4_Fill2 = "";

        //  Fields making up the AJ1 element
        static int iAJ1_IndexOf = 0;
        static int iAJ1_Length = 17;
        static string sAJ1 = "";
        static string sAJ1_Fill1 = "";
        static string sAJ1_Sd = "";
        static string sAJ1_Fill2 = "";

        //  Fields making up the AW1-4 elements
        static int iAW1_IndexOf = 0;
        static int iAW1_Length = 6;
        static string sAW1 = "";
        static string sAW1_Fill1 = "";
        static string sAW1_Zz = "";
        static string sAW1_Fill2 = "";

        static int iAW2_IndexOf = 0;
        static int iAW2_Length = 6;
        static string sAW2 = "";
        static string sAW2_Fill1 = "";
        static string sAW2_Zz = "";
        static string sAW2_Fill2 = "";

        static int iAW3_IndexOf = 0;
        static int iAW3_Length = 6;
        static string sAW3 = "";
        static string sAW3_Fill1 = "";
        static string sAW3_Zz = "";
        static string sAW3_Fill2 = "";

        static int iAW4_IndexOf = 0;
        static int iAW4_Length = 6;
        static string sAW4 = "";
        static string sAW4_Fill1 = "";
        static string sAW4_Zz = "";
        static string sAW4_Fill2 = "";

        static string separator = "\t";

        /** should the output be converted to imperial from metric? **/
        static bool convertToImperial = false;

        static string sHeader =
                "USAF_ID" + separator +
                "WBAN_ID" + separator +
                "OBERVATION_DATE" + separator +
                "WIND_DIR_360" + separator +
                "WIND_SPEED_MS" + separator +
                "WIND_GUST_MS" + separator +
                "SKY_CEILING_M" + separator +
                "SKY_COVER_CODE" + separator +
                "SKY_COND_LOW_CODE" + separator +
                "SKY_COND_MID_CODE" + separator +
                "SKY_COND_HIGH_CODE" + separator +
                "VISIBILITY_M" + separator +
                "WEATHER_OBS_MANUAL1_CODE" + separator +
                "WEATHER_OBS_MANUAL2_CODE" + separator +
                "WEATHER_OBS_MANUAL3_CODE" + separator +
                "WEATHER_OBS_MANUAL4_CODE" + separator +
                "WEATHER_OBS_AUTO1_CODE" + separator +
                "WEATHER_OBS_AUTO2_CODE" + separator +
                "WEATHER_OBS_AUTO3_CODE" + separator +
                "WEATHER_OBS_AUTO4_CODE" + separator +
                "W" + separator +
                "TEMPERATURE_C" + separator +
                "DEW_POINT_C" + separator +
                "PRESSURE_SEA_LEVEL_HP" + separator +
                "PRESSURE_ALTIMETER_HP" + separator +
                "PRESSURE_STATION_HP" + separator +
                "MAX" + separator +
                "MIN" + separator +
                "PRECIP_LAST_01HR_MM" + separator +
                "PRECIP_LAST_06HR_MM" + separator +
                "PRECIP_LAST_12HR_MM" + separator +
                "PRECIP_LAST_24HR_MM" + separator +
                "SNOW_DEPTH_MM"
                + "\n";


        public static void ConvertISHFile(string fileName)
        {
            //        logIt(fDebug, iPROD, false, "---------------------------- Begin "+sProgramName);          // Append output to log.
            //        logIt(fDebug, iPROD, false, "Number of args found=["+args.Length+"]");                    // Append output to log.

            // Process args
            sInFileName = fileName;
            sOutFileName = fileName + ".csv";

            //        sOutFileName  = sInFileName+".java.out";

            logIt(fDebug, iDEBUG, false, "        Input Filename=[" + sInFileName + "]");                // Append output to log.
            logIt(fDebug, iDEBUG, false, "       Output Filename=[" + sOutFileName + "]");               // Append output to log.
            logIt(fDebug, iDEBUG, false, "         Logging Level=[" + iLogLevel + "]");                  // Append output to log.
            logIt(fDebug, iDEBUG, false, "1st Log Message Filter=[" + p_sFilter1 + "]");
            logIt(fDebug, iDEBUG, false, "2nd Log Message Filter=[" + p_sFilter2 + "]");
            // End of args

            try
            {
                StreamReader fInReader = new StreamReader(sInFileName);
                StreamWriter fFixedWriter = new StreamWriter(sOutFileName);
                fFixedWriter.Write(sHeader);           // Put header into output file.

                try
                {
                    string line = null;
                    while ((line = fInReader.ReadLine()) != null)
                    {
                        iCounter++;
                        //                    iOffset         = 25;
                        iLength = line.Length;
                        //                    logIt(fDebug, iDEBUG, false, "Record # "+iCounter+" had iLength=["+iLength+"]");
                        //                    Console.WriteLine(line);

                        // See where the REM section begins
                        iREM_IndexOf = line.IndexOf("REM");
                        if (iREM_IndexOf == -1)
                            iREM_IndexOf = 9999;      // If no REM section then set to high value

                        getCDS(line);   //  Fields making up the Control Data Section.

                        sConcat = sCDS_ID + "-" + sCDS_Wban + "-" + sCDS_Year + "-" + sCDS_Month + "-" + sCDS_Day + " " + sCDS_Hour + ":" + sCDS_Minute;
                        sConcatDate = sCDS_Year + "-" + sCDS_Month + "-" + sCDS_Day;
                        sConcatMonth = sCDS_Year + "-" + sCDS_Month;


                        // =-=-=-=-=-=-=-=-=-=-=-=-=-= Filter out all but a certain station/date =-=-=-=-=-=-=-=-=-=-=-=-=-=
                        //                    if ( (! sConcatDate.Equals("2011-01-01")) && (! sConcatDate.Equals("2010-01-02")) )
                        //                    if ( (! sConcatDate.Equals("2012-04-12")) )           // Whole Day
                        //                    if ( (! sConcatMonth.Equals("2009-04")) )           // Whole month
                        //                    {
                        //                        continue;
                        //                    }
                        //
                        //                    logIt(fDebug, iDEBUG, false, "line=["+line+"] ");
                        //
                        //                    logIt(fDebug, iDEBUG, false, "Record # "+iCounter+" had sConcat=["+sConcat+"]");
                        //
                        //                    if (iCounter >= 100)
                        //                    {
                        //                        logIt(fDebug, iDEBUG, false, "Max count reached.  Stopping...");
                        //                        fFixedWriter.flush();
                        //                        fFixedWriter.close();
                        //                        System.exit(22);
                        //                    }
                        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= Done =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=


                        getMDS(line);   //  Fields making up the Mandatory Data Section.
                        getOC1(line);   //  Fields making up the OC1 element.
                        getGF1(line);   //  Fields making up the GF1 element.
                        getMW1(line);   //  Fields making up the MW1 element.
                        getMW2(line);   //  Fields making up the MW2 element.
                        getMW3(line);   //  Fields making up the MW3 element.
                        getMW4(line);   //  Fields making up the MW3 element.       // 06/21/2012  ras
                        getAY1(line);   //  Fields making up the AY1 element.
                        getMA1(line);   //  Fields making up the MA1 element.
                        sMaxTemp = "";
                        sMinTemp = "";
                        getKA1(line);   //  Fields making up the KA1 element.
                        getKA2(line);   //  Fields making up the KA2 element.
                        sPcp01 = "";
                        sPcp06 = "";
                        sPcp12 = "";
                        sPcp24 = "";
                        getAA1(line);   //  Fields making up the AA1 element.
                        getAA2(line);   //  Fields making up the AA2 element.
                        getAA3(line);   //  Fields making up the AA3 element.
                        getAA4(line);   //  Fields making up the AA4 element.
                        getAJ1(line);   //  Fields making up the AJ1 element.
                        getAW1(line);   //  Fields making up the AW1 element.       // 06/06/2012  ras
                        getAW2(line);   //  Fields making up the AW2 element.       // 06/06/2012  ras
                        getAW3(line);   //  Fields making up the AW3 element.       // 06/06/2012  ras
                        getAW4(line);   //  Fields making up the AW4 element.       // 06/06/2012  ras

                        // Begin formatting output record..............................................................

                        // Build Control Data Section
                        sControlSection = sCDS_ID + separator
                                + sCDS_Wban + separator
                                + sCDS_Year + sCDS_Month + sCDS_Day + sCDS_Hour + sCDS_Minute;

                        // Sort Present Weather elements
                        sWW1234 = new string[] { sMW1_Ww, sMW2_Ww, sMW3_Ww, sMW4_Ww };
                        Array.Sort(sWW1234);

                        // Sort Present Weather (Automated) elements
                        sAW1234 = new string[] { sAW1_Zz, sAW2_Zz, sAW3_Zz, sAW4_Zz };
                        Array.Sort(sAW1234);

                        // Build Mandatory Data Section + the rest of the record
                        sMandatorySection = sMDS_Dir + separator
                                + sMDS_Spd + separator
                                + sOC1_Gus + separator
                                + sMDS_Clg + separator
                                + sGF1_Skc + separator
                                + sGF1_Low + separator
                                + sGF1_Med + separator
                                + sGF1_Hi + separator
                                + sMDS_Vsb + separator
                                + sWW1234[3] + separator
                                + sWW1234[2] + separator
                                + sWW1234[1] + separator
                                + sWW1234[0] + separator
                                + sAW1234[3] + separator
                                + sAW1234[2] + separator
                                + sAW1234[1] + separator
                                + sAW1234[0] + separator
                                + sAY1_Pw + separator
                                + sMDS_Temp + separator
                                + sMDS_Dewp + separator
                                + sMDS_Slp + separator
                                + sMA1_Alt + separator
                                + sMA1_Stp + separator
                                + sMaxTemp + separator
                                + sMinTemp + separator
                                + sPcp01 + sPcp01t + separator
                                + sPcp06 + sPcp06t + separator
                                + sPcp12 + sPcp12t + separator
                                + sPcp24 + sPcp24t + separator
                                + sAJ1_Sd;

                        sOutputRecord = sControlSection + separator + sMandatorySection;  // Put it all together
                        fFixedWriter.Write(sOutputRecord + "\n");                 // Write out the record

                    }  // while read

                }
                catch (IOException ex)
                {
                    Console.Error.WriteLine(sProgramName + ": IOException 2. Error=[" + ex.Message + "]");
                    Console.Error.WriteLine(sProgramName + ": Stack trace follows:\n" + ex.StackTrace);
                    Environment.Exit(2);
                }

                fInReader.Close();
                fFixedWriter.Flush();
                fFixedWriter.Close();

            }
            catch (Exception e)
            {                                                               //Catch exception if any
                sMessage = sProgramName + ": Unspecified Exception 1. Error=[" + e.Message + "]";
                bStdErr = true;
                logIt(fDebug, iPROD, false, sMessage);         // Append output to log.
                Console.Error.WriteLine(sProgramName + ": Stack trace follows:\n" + e.StackTrace);
                Environment.Exit(1);
            }

            logIt(fDebug, iDEBUG, false, "Processed " + iCounter + " records");
            logIt(fDebug, iDEBUG, false, "Done.");

        }   // End of Main()

        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // formatInt - Right-justifies an int.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static string formatInt(int i, int len)
        {
            string blanks = "                 ";
            string s = i.ToString();
            if (s.Length < len)
                s = blanks.Substring(0, len - s.Length) + s;
            return s;
        }


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // formatFloat - Right-justifies a float.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static string formatFloat(float i, int len)
        {
            string blanks = "                 ";
            string s = i.ToString();
            if (s.Length < len)
                s = blanks.Substring(0, len - s.Length) + s;
            return s;
        }



        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getCDS - Get CDS section and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getCDS(string p_sRecd)
        {
            //  Extract fields making up the Control Data Section.
            sCDS = p_sRecd.Substring(0, 60);
            sCDS_Fill1 = p_sRecd.Substring(0, 4);
            sCDS_ID = p_sRecd.Substring(4, 10);
            sCDS_Wban = p_sRecd.Substring(10, 15);
            sCDS_Year = p_sRecd.Substring(15, 19);
            sCDS_Month = p_sRecd.Substring(19, 21);
            sCDS_Day = p_sRecd.Substring(21, 23);
            sCDS_Hour = p_sRecd.Substring(23, 25);
            sCDS_Minute = p_sRecd.Substring(25, 27);
            sCDS_Fill2 = p_sRecd.Substring(27, 60);
        }  // End of getCDS


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getMDS - Get MDS section and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getMDS(string p_sRecd)
        {
            //  Extract fields making up the Mandatory Data Section.
            sMDS = p_sRecd.Substring(60, 46);
            sMDS_Dir = p_sRecd.Substring(60, 4);
            sMDS_DirQ = p_sRecd.Substring(63, 2);
            sMDS_DirType = p_sRecd.Substring(64, 2);
            sMDS_Spd = p_sRecd.Substring(65, 5);
            sMDS_Fill2 = p_sRecd.Substring(69, 2);
            sMDS_Clg = p_sRecd.Substring(70, 6);
            sMDS_Fill3 = p_sRecd.Substring(75, 4);
            sMDS_Vsb = p_sRecd.Substring(78, 7);
            sMDS_Fill4 = p_sRecd.Substring(84, 4);
            sMDS_TempSign = p_sRecd.Substring(87, 2);
            sMDS_Temp = p_sRecd.Substring(88, 5);
            sMDS_Fill5 = p_sRecd.Substring(92, 2);
            sMDS_DewpSign = p_sRecd.Substring(93, 2);
            sMDS_Dewp = p_sRecd.Substring(94, 5);
            sMDS_Fill6 = p_sRecd.Substring(98, 2);
            sMDS_Slp = p_sRecd.Substring(99, 6);
            sMDS_Fill7 = p_sRecd.Substring(104, 2);

            if (sMDS_Dir.Equals("999"))
                sMDS_Dir = "";

            if (sMDS_DirType.Equals("V"))
                sMDS_Dir = "990";

            //        logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sMDS_Dir=["+sMDS_Dir+"] sMDS_DirQ=["+sMDS_DirQ+"] sMDS_DirType=["+sMDS_DirType+"]");     // temporary - ras

            // wind speed
            if (sMDS_Spd.Equals("9999"))
                sMDS_Spd = "";
            else
            {
                //                      Console.WriteLine("sMDS=["+sMDS+"] Spd=["+sMDS_Spd+"]");
                iWork = int.Parse(sMDS_Spd);                   // Convert to integer
                
                if (convertToImperial)
                {
                    //                      Console.WriteLine("iWork=["+iWork+"]");
                    iWork = (int)(iWork / 10.0f * 2.237f + .5f);    // Convert Meters Per Second to Miles Per Hour
                                                                          //                      Console.WriteLine("iWork=["+iWork+"]");
                                                                          //                      sMDS_Spd  = fmt3.format(iWork);
                    sMDS_Spd = formatInt(iWork, 3);
                    //                      Console.WriteLine("Spd=["+sMDS_Spd+"]");
                    //          logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sMDS_Spd=["+sMDS_Spd+"]");     // temporary - ras
                }
                else
                {
                    iWork = (int)(iWork / 10.0f);
                    sMDS_Spd = formatInt(iWork, 3);
                }

            }

            // cloud ceiling
            if (sMDS_Clg.Equals("99999"))
                sMDS_Clg = "";
            else
            {
                if (convertToImperial)
                {
                    try
                    {
                        iWork = int.Parse(sMDS_Clg);                 // Convert to integer
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] sMDS_Clg value could not be converted to integer=[" + sMDS_Clg + "]");
                        sMDS_Clg = "";                                     // Data error.  Set to missing.
                    }
                    if (!sMDS_Clg.Equals(""))
                    {
                        iWork = (int)(iWork * 3.281f / 100.0f + .5f);   // Convert Meters to Hundreds of Feet
                        sMDS_Clg = formatInt(iWork, 3);
                        //              logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sMDS_Clg=["+sMDS_Clg+"]");     // temporary - ras
                    }
                }
            }

            // visibility in meters
            if (sMDS_Vsb.Equals("999999"))
                sMDS_Vsb = "";
            else
            {
                fWork = float.Parse(sMDS_Vsb);                 // Convert to floating point
                if (convertToImperial)
                {
                    fWork = (fWork * 0.000625f);                // Convert Meters to Miles using CDO's value
                                                                               //            fWork     = ((float)(fWork * (float) 0.000621371192237334));    // Convert Meters to Miles
                    if (fWork > 99.9f)
                        fWork = 99.0f;

                    if (fWork == 10.058125f)                          // Match CDO       2011-04-28  ras
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] sMDS_Vsb value rounded to 10 miles");
                        fWork = 10.0f;
                    }
                }
                fWorkSave = fWork;                                      // Save this value for possible display
                sMDS_Vsb = fWork.ToString(fmt4_1).Substring(0, 4);
            }

            // extreme temperature in C, recorded for the observation interval
            if (sMDS_Temp.Equals("9999"))
                sMDS_Temp = "";
            else
            {
                //                        Console.WriteLine(sMDS_Temp);
                iWork = int.Parse(sMDS_Temp);     // Convert to integer
                if (sMDS_TempSign.Equals("-"))
                    iWork *= -1;

                if (convertToImperial)
                {
                    if (iWork < -178)
                        iWork = (int)(iWork / 10.0f * 1.8f + 32.0f - .5f);  // Handle temps below 0F
                    else
                        iWork = (int)(iWork / 10.0f * 1.8f + 32.0f + .5f);
                }
                else
                    iWork = (int)(iWork / 10.0f);
                sMDS_Temp = formatInt(iWork, 4);
                //                        Console.WriteLine(sMDS_Temp);
            }

            // dewpoint in C
            if (sMDS_Dewp.Equals("9999"))
                sMDS_Dewp = "";
            else
            {
                //                        Console.WriteLine(sMDS_Dewp);
                iWork = int.Parse(sMDS_Dewp);     // Convert to integer
                if (sMDS_DewpSign.Equals("-"))
                    iWork *= -1;
                if (convertToImperial)
                {
                    if (iWork < -178)
                        iWork = (int)(iWork / 10.0f * 1.8f + 32.0f - .5f);  // Handle temps below 0F
                    else
                        iWork = (int)(iWork / 10.0f * 1.8f + 32.0f + .5f);
                }
                else
                    iWork = (int)(((float)iWork / 10.0));
                sMDS_Dewp = formatInt(iWork, 4);
                //                        Console.WriteLine(sMDS_Dewp);
            }

            // sea level pressure, HP
            if (sMDS_Slp.Equals("99999"))
                sMDS_Slp = "";
            else
            {
                fWork = float.Parse(sMDS_Slp);                 // Convert to floating point
                fWork = (fWork / 10.0f);                    // Convert convert Hectopascals to Millibars
                sMDS_Slp = fWork.ToString(fmt6_1).Substring(0, 6);
            }
        }  // End of getMDS


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getOC1 - Get OC1 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getOC1(string p_sRecd)
        {
            sOC1 = "";
            sOC1_Fill1 = "";
            sOC1_Gus = "";
            sOC1_Fill2 = "";
            iOC1_IndexOf = p_sRecd.IndexOf("OC1");
            if ((iOC1_IndexOf >= 0) && (iOC1_IndexOf < iREM_IndexOf))
            {
                sOC1 = p_sRecd.Substring(iOC1_IndexOf, iOC1_Length);
                sOC1_Fill1 = sOC1.Substring(1, 3);  // 3
                sOC1_Gus = sOC1.Substring(3, 4);  // 4
                sOC1_Fill2 = sOC1.Substring(7, 1);  // 1

                if (sOC1_Gus.Equals("9999"))
                    sOC1_Gus = "";
                    //                logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sOC1_Gus missing=["+sOC1_Gus+"]");     // temporary - ras
                else
                {
                    try
                    {
                        iWork = int.Parse(sOC1_Gus);                   // Convert to integer
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] sOC1_Gus value could not be converted to integer=[" + sOC1_Gus + "]");
                        sOC1_Gus = "";             // Data error.  Set to missing.
                    }
                    if (!sOC1_Gus.Equals(""))
                    {
                        if (convertToImperial)
                        {
                            iWork = (int)(iWork / 10.0f * 2.237f + .5f);    // Convert Meters Per Second to Miles Per Hour
                            sOC1_Gus = formatInt(iWork, 3);
                            //                    logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sOC1_Gus=["+sOC1_Gus+"]");     // temporary - ras
                        }
                        else
                        {
                            iWork = (int)(iWork / 10.0f);
                            sOC1_Gus = formatInt(iWork, 3);
                        }
                    }
                }
            }
        }  // End of getOC1


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getGF1 - Get GF1 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getGF1(string p_sRecd)
        {
            sGF1 = "";
            sGF1_Fill1 = "";
            sGF1_Skc = "";
            sGF1_Fill2 = "";
            sGF1_Low = "";
            sGF1_Fill3 = "";
            sGF1_Med = "";
            sGF1_Fill4 = "";
            sGF1_Hi = "";
            sGF1_Fill5 = "";
            iGF1_IndexOf = p_sRecd.IndexOf("GF1");
            if ((iGF1_IndexOf >= 0) && (iGF1_IndexOf < iREM_IndexOf))
            {
                sGF1 = p_sRecd.Substring(iGF1_IndexOf, iGF1_Length);
                sGF1_Fill1 = sGF1.Substring(1, 3);
                sGF1_Skc = sGF1.Substring(3, 3);
                sGF1_Fill2 = sGF1.Substring(5, 7);
                sGF1_Low = sGF1.Substring(11, 3);
                sGF1_Fill3 = sGF1.Substring(13, 8);
                sGF1_Med = sGF1.Substring(20, 3);
                sGF1_Fill4 = sGF1.Substring(22, 2);
                sGF1_Hi = sGF1.Substring(23, 3);
                sGF1_Fill5 = sGF1.Substring(25, 2);
            }

            if ((iGF1_IndexOf >= 0) && (iGF1_IndexOf < iREM_IndexOf))
            {
                if (sGF1_Skc.Equals("99"))
                    sGF1_Skc = "";
                else
                {
                    //                            Console.WriteLine("DateTime=["+sConcat+"] GF1=["+sGF1+"]  Skc=["+sGF1_Skc+"]");
                    try
                    {
                        iWork = int.Parse(sGF1_Skc);   // Convert to integer
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] sGF1_Skc value could not be converted to integer=[" + sGF1_Skc + "]");
                        sGF1_Skc = "***";                                     // Data error.  Set to missing.
                    }
                    if (!sGF1_Skc.Equals("***"))
                    {
                        if (iWork == 0)
                            sGF1_Skc = "CLR";
                        else 
                            if ((iWork >= 1) && (iWork <= 4))
                                sGF1_Skc = "SCT";
                            else
                                if ((iWork >= 5) && (iWork <= 7))
                                    sGF1_Skc = "BKN";
                                else
                                    if (iWork == 8)
                                        sGF1_Skc = "OVC";
                                    else
                                        if (iWork == 9)
                                            sGF1_Skc = "OBS";
                                        else
                                            if (iWork == 10)
                                                sGF1_Skc = "POB";
                    }
                }
                if (sGF1_Low.Equals("99"))       // Low cloud type
                    sGF1_Low = "";
                else
                    sGF1_Low = sGF1_Low.Substring(1, 2);

                if (sGF1_Med.Equals("99"))       // Med cloud type
                    sGF1_Med = "";
                else
                    sGF1_Med = sGF1_Med.Substring(1, 2);

                if (sGF1_Hi.Equals("99"))        // High cloud type
                    sGF1_Hi = "";
                else
                    sGF1_Hi = sGF1_Hi.Substring(1, 2);
            }
        }  // End of getGF1


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getMW1 - Get MW1 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getMW1(string p_sRecd)
        {
            sMW1 = "";
            sMW1_Fill1 = "";
            sMW1_Ww = "";
            sMW1_Fill2 = "";
            iMW1_IndexOf = p_sRecd.IndexOf("MW1");
            if ((iMW1_IndexOf >= 0) && (iMW1_IndexOf < iREM_IndexOf))
            {
                sMW1 = p_sRecd.Substring(iMW1_IndexOf, iMW1_Length);
                sMW1_Fill1 = sMW1.Substring(1, 3);  // 3
                sMW1_Ww = sMW1.Substring(3, 2);  // 2
                sMW1_Fill2 = sMW1.Substring(5, 1);  // 1
                                                    //                        Console.WriteLine("MW1=["+sMW1+"] Ww=["+sMW1_Ww+"]");
            }
        }  // End of getMW1


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getMW2 - Get MW2 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getMW2(string p_sRecd)
        {
            sMW2 = "";
            sMW2_Fill1 = "";
            sMW2_Ww = "";
            sMW2_Fill2 = "";
            iMW2_IndexOf = p_sRecd.IndexOf("MW2");
            if ((iMW2_IndexOf >= 0) && (iMW2_IndexOf < iREM_IndexOf))
            {
                sMW2 = p_sRecd.Substring(iMW2_IndexOf, iMW2_Length);
                sMW2_Fill1 = sMW2.Substring(1, 3);  // 3
                sMW2_Ww = sMW2.Substring(3, 2);  // 2
                sMW2_Fill2 = sMW2.Substring(5, 1);  // 1
                                                    //                        Console.WriteLine("MW2=["+sMW2+"] Ww=["+sMW2_Ww+"]");
            }
        }  // End of getMW2


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getMW3 - Get MW3 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getMW3(string p_sRecd)
        {
            sMW3 = "";
            sMW3_Fill1 = "";
            sMW3_Ww = "";
            sMW3_Fill2 = "";
            iMW3_IndexOf = p_sRecd.IndexOf("MW3");
            if ((iMW3_IndexOf >= 0) && (iMW3_IndexOf < iREM_IndexOf))
            {
                sMW3 = p_sRecd.Substring(iMW3_IndexOf, iMW3_Length);
                sMW3_Fill1 = sMW3.Substring(1, 3);  // 3
                sMW3_Ww = sMW3.Substring(3, 2);  // 2
                sMW3_Fill2 = sMW3.Substring(5, 1);  // 1
                                                    //                        Console.WriteLine("MW3=["+sMW3+"] Ww=["+sMW3_Ww+"]");
            }
        }  // End of getMW3


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getMW4 - Get MW4 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getMW4(string p_sRecd)
        {
            sMW4 = "";
            sMW4_Fill1 = "";
            sMW4_Ww = "";
            sMW4_Fill2 = "";
            iMW4_IndexOf = p_sRecd.IndexOf("MW4");
            if ((iMW4_IndexOf >= 0) && (iMW4_IndexOf < iREM_IndexOf))
            {
                sMW4 = p_sRecd.Substring(iMW4_IndexOf, iMW4_Length);
                sMW4_Fill1 = sMW4.Substring(1, 3);  // 3
                sMW4_Ww = sMW4.Substring(3, 2);  // 2
                sMW4_Fill2 = sMW4.Substring(5, 1);  // 1
                                                    //            logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sMW4_Ww=["+sMW4_Ww+"]");     // temporary - ras
            }
        }  // End of getMW4


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAY1 - Get AY1 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAY1(string p_sRecd)
        {
            sAY1 = "";
            sAY1_Fill1 = "";
            sAY1_Pw = "";
            sAY1_Fill2 = "";
            iAY1_IndexOf = p_sRecd.IndexOf("AY1");
            if ((iAY1_IndexOf >= 0) && (iAY1_IndexOf < iREM_IndexOf))
            {
                sAY1 = p_sRecd.Substring(iAY1_IndexOf, iAY1_Length);
                sAY1_Fill1 = sAY1.Substring(1, 3);  // 3
                sAY1_Pw = sAY1.Substring(3, 1);  // 1
                sAY1_Fill2 = sAY1.Substring(4, 4);  // 4
                                                    //                        Console.WriteLine("AY1=["+sAY1+"] Pw=["+sAY1_Pw+"]");
            }
        }  // End of getAY1


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getMA1 - Get MA1 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getMA1(string p_sRecd)
        {
            sMA1 = "";
            sMA1_Fill1 = "";
            sMA1_Alt = "";
            sMA1_Fill2 = "";
            sMA1_Stp = "";
            sMA1_Fill3 = "";
            iMA1_IndexOf = p_sRecd.IndexOf("MA1");
            if ((iMA1_IndexOf >= 0) && (iMA1_IndexOf < iREM_IndexOf))
            {
                sMA1 = p_sRecd.Substring(iMA1_IndexOf, iMA1_Length);
                sMA1_Fill1 = sMA1.Substring(1, 3);      // 3
                sMA1_Alt = sMA1.Substring(3, 5);      // 5
                sMA1_Fill2 = sMA1.Substring(8, 1);      // 1
                sMA1_Stp = sMA1.Substring(9, 5);     // 5
                sMA1_Fill3 = sMA1.Substring(14, 1);    // 1

                if (sMA1_Alt.Equals("99999"))
                    sMA1_Alt = "";
                else
                {
                    try
                    {
                        fWork = float.Parse(sMA1_Alt);                 // Convert to floating point
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] sMA1_Alt value could not be converted to floating point=[" + sMA1_Alt + "]");
                        sMA1_Alt = "";                                      // Data error.  Set to missing.
                    }
                    if (!sMA1_Alt.Equals(""))
                    {
                        if (convertToImperial)
                            fWork = fWork / 10.0f * 100.0f / 3386.39f;    // Convert Hectopascals to Inches
                        else
                            fWork /= 10.0f;
                        sMA1_Alt = fWork.ToString(fmt5_2).Substring(0, 5);
                    }
                }
                if (sMA1_Stp.Equals("99999"))
                    sMA1_Stp = "";
                else
                {
                    try
                    {
                        fWork = float.Parse(sMA1_Stp);                 // Convert to floating point
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] sMA1_Stp value could not be converted to floating point=[" + sMA1_Stp + "]");
                        sMA1_Stp = "******";                                     // Data error.  Set to missing.
                    }
                    if (!sMA1_Stp.Equals("******"))
                    {
                        fWork /= 10.0f;                    // Convert convert Hectopascals to Millibars
                        sMA1_Stp = fWork.ToString(fmt6_1).Substring(0, 6);
                    }
                }
            }
        }  // End of getMA1


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getKA1 - Get KA1 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getKA1(string p_sRecd)
        {
            sKA1 = "";
            sKA1_Fill1 = "";
            sKA1_Code = "";
            sKA1_Temp = "";
            sKA1_Fill2 = "";
            iKA1_IndexOf = p_sRecd.IndexOf("KA1");
            if ((iKA1_IndexOf >= 0) && (iKA1_IndexOf < iREM_IndexOf))
            {
                sKA1 = p_sRecd.Substring(iKA1_IndexOf, iKA1_IndexOf + iKA1_Length);
                sKA1_Fill1 = sKA1.Substring(1, 6);   // 6
                sKA1_Code = sKA1.Substring(6, 1);   // 1
                sKA1_Temp = sKA1.Substring(7, 5);  // 5
                sKA1_Fill2 = sKA1.Substring(12, 1); // 1
                                                     //                        Console.WriteLine("KA1=["+sKA1+"] Code=["+sKA1_Code+"] Temp=["+sKA1_Temp+"]");
                if (sKA1_Temp.Equals("+9999"))
                    sKA1_Temp = "";
                else
                {
                    try
                    {
                        fWork = float.Parse(sKA1_Temp);                  // Convert to floating point
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] sKA1_Temp value could not be converted to floating point=[" + sKA1_Temp + "]");
                        sKA1_Temp = "";                                         // Data error.  Set to missing.
                    }
                    if (!sKA1_Temp.Equals(""))
                    {
                        if (convertToImperial)
                        {
                            if (fWork < -178)
                                fWork = (int)(fWork / 10.0f * 1.8f + 32.0f - .5f);  // Handle temps below 0F
                            else
                                fWork = (int)(fWork / 10.0f * 1.8f + 32.0f + .5f);
                        }
                        else
                            fWork = (int)(fWork / 10.0f);

                        if (sKA1_Code.Equals("N"))
                            sMinTemp = formatInt((int)fWork, 3);
                        else if (sKA1_Code.Equals("M"))
                                sMaxTemp = formatInt((int)fWork, 3);
                    }
                }
            }
        }  // End of getKA1


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getKA2 - Get KA2 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getKA2(string p_sRecd)
        {
            sKA2 = "";
            sKA2_Fill1 = "";
            sKA2_Code = "";
            sKA2_Temp = "";
            sKA2_Fill2 = "";
            iKA2_IndexOf = p_sRecd.IndexOf("KA2");
            if ((iKA2_IndexOf >= 0) && (iKA2_IndexOf < iREM_IndexOf))
            {
                sKA2 = p_sRecd.Substring(iKA2_IndexOf, iKA2_Length);
                sKA2_Fill1 = sKA2.Substring(1, 6);   // 6
                sKA2_Code = sKA2.Substring(6, 1);   // 1
                sKA2_Temp = sKA2.Substring(7, 5);  // 5
                sKA2_Fill2 = sKA2.Substring(12, 1); // 1
                                                     //                        Console.WriteLine("KA2=["+sKA2+"] Code=["+sKA2_Code+"] Temp=["+sKA2_Temp+"]");
                if (sKA2_Temp.Equals("+9999"))
                    sKA2_Temp = "";
                else
                {
                    try
                    {
                        fWork = float.Parse(sKA2_Temp);                 // Convert to floating point
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] sKA2_Temp value could not be converted to floating point=[" + sKA2_Temp + "]");
                        sKA2_Temp = "";             // Data error.  Set to missing.
                    }
                    if (!sKA2_Temp.Equals(""))
                    {
                        if (convertToImperial)
                        {
                            if (fWork < -178)
                                fWork = (int)(fWork / 10.0f * 1.8f + 32.0f - .5f);  // Handle temps below 0F
                            else
                                fWork = (int)(fWork / 10.0f * 1.8f + 32.0f + .5f);
                        }
                        else
                            fWork = (int)(fWork / 10.0f);

                        if (sKA2_Code.Equals("N"))
                            sMinTemp = formatInt((int)fWork, 3);
                        else if (sKA2_Code.Equals("M"))
                                sMaxTemp = formatInt((int)fWork, 3);
                    }
                }
            }
        }  // End of getKA2


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAA1 - Get AA1 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAA1(string p_sRecd)
        {
            float fWork = 0.0f;
            sAA1 = "";
            sAA1_Fill1 = "";
            sAA1_Hours = "";
            sAA1_Pcp = "";
            sAA1_Trace = "";
            sAA1_Fill2 = "";
            iAA1_IndexOf = p_sRecd.IndexOf("AA1");
            if ((iAA1_IndexOf >= 0) && (iAA1_IndexOf < iREM_IndexOf))
            {
                sAA1 = p_sRecd.Substring(iAA1_IndexOf, iAA1_Length);
                sAA1_Fill1 = sAA1.Substring(1, 3);   // 3
                sAA1_Hours = sAA1.Substring(3, 2);   // 2
                sAA1_Pcp = sAA1.Substring(5, 4);   // 4
                sAA1_Trace = sAA1.Substring(9, 1);  // 1
                sAA1_Fill2 = sAA1.Substring(10, 1); // 1
                                                     //                        Console.WriteLine("AA1=["+sAA1+"] Pcp=["+sAA1_Pcp+"]");
                if (sAA1_Pcp.Equals("9999"))
                    sAA1_Pcp = "";
                else
                {
                    try
                    {
                        fWork = float.Parse(sAA1_Pcp);       // Convert to floating point
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] AA1_Pcp value could not be converted to floating point=[" + sAA1_Pcp + "]");
                        sAA1_Pcp = "";             // Data error.  Set to missing.
                    }
                    if (!sAA1_Pcp.Equals(""))
                        setPcp(fWork, sAA1_Hours, sAA1_Trace);
                }
            }
        }  // End of getAA1


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAA2 - Get AA2 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAA2(string p_sRecd)
        {
            float fWork = 0.0f;
            sAA2 = "";
            sAA2_Fill1 = "";
            sAA2_Hours = "";
            sAA2_Pcp = "";
            sAA2_Trace = "";
            sAA2_Fill2 = "";
            iAA2_IndexOf = p_sRecd.IndexOf("AA2");
            if ((iAA2_IndexOf >= 0) && (iAA2_IndexOf < iREM_IndexOf))
            {
                //                        Console.WriteLine("DateTime=["+sConcat+"] iAA2_IndexOf=["+iAA2_IndexOf+"] iAA2_Length=["+iAA2_Length+"] Line Length=["+iLength+"]");
                sAA2 = p_sRecd.Substring(iAA2_IndexOf, iAA2_Length);
                sAA2_Fill1 = sAA2.Substring(1, 3);   // 3
                sAA2_Hours = sAA2.Substring(3, 2);   // 2
                sAA2_Pcp = sAA2.Substring(5, 4);   // 4
                sAA2_Trace = sAA2.Substring(9, 1);  // 1
                sAA2_Fill2 = sAA2.Substring(10, 1); // 1
                                                     //                        Console.WriteLine("AA2=["+sAA2+"] Pcp=["+sAA2_Pcp+"]");
                if (sAA2_Pcp.Equals("9999"))
                {
                    sAA2_Pcp = "";
                }
                else
                {
                    try
                    {
                        fWork = float.Parse(sAA2_Pcp);       // Convert to floating point
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] AA2_Pcp value could not be converted to floating point=[" + sAA2_Pcp + "]");
                        sAA2_Pcp = "*****";             // Data error.  Set to missing.
                    }
                    if (!sAA2_Pcp.Equals("*****"))
                    {
                        setPcp(fWork, sAA2_Hours, sAA2_Trace);
                    }
                }
            }
        }  // End of getAA2


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAA3 - Get AA3 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAA3(string p_sRecd)
        {
            float fWork = 0.0f;
            sAA3 = "";
            sAA3_Fill1 = "";
            sAA3_Hours = "";
            sAA3_Pcp = "";
            sAA3_Trace = "";
            sAA3_Fill2 = "";
            iAA3_IndexOf = p_sRecd.IndexOf("AA3");
            if ((iAA3_IndexOf >= 0) && (iAA3_IndexOf < iREM_IndexOf))
            {
                //                        Console.WriteLine("DateTime=["+sConcat+"] iAA3_IndexOf=["+iAA3_IndexOf+"] iAA3_Length=["+iAA3_Length+"] Line Length=["+iLength+"]");
                sAA3 = p_sRecd.Substring(iAA3_IndexOf, iAA3_Length);
                sAA3_Fill1 = sAA3.Substring(1, 3);   // 3
                sAA3_Hours = sAA3.Substring(3, 2);   // 2
                sAA3_Pcp = sAA3.Substring(5, 4);   // 4
                sAA3_Trace = sAA3.Substring(9, 1);  // 1
                sAA3_Fill2 = sAA3.Substring(10, 1); // 1
                                                     //                        Console.WriteLine("AA3=["+sAA3+"] Pcp=["+sAA3_Pcp+"]");
                if (sAA3_Pcp.Equals("9999"))
                {
                    sAA3_Pcp = "";
                }
                else
                {
                    try
                    {
                        fWork = float.Parse(sAA3_Pcp);       // Convert to floating point
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iPROD, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] AA3_Pcp value could not be converted to floating point=[" + sAA3_Pcp + "]");
                        sAA3_Pcp = "";             // Data error.  Set to missing.
                    }
                    if (!sAA3_Pcp.Equals(""))
                    {
                        setPcp(fWork, sAA3_Hours, sAA3_Trace);
                    }
                }
            }
        }  // End of getAA3


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAA4 - Get AA4 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAA4(string p_sRecd)
        {
            float fWork = 0.0f;
            sAA4 = "";
            sAA4_Fill1 = "";
            sAA4_Hours = "";
            sAA4_Pcp = "";
            sAA4_Trace = "";
            sAA4_Fill2 = "";
            iAA4_IndexOf = p_sRecd.IndexOf("AA4");
            if ((iAA4_IndexOf >= 0) && (iAA4_IndexOf < iREM_IndexOf))
            {
                //                        Console.WriteLine("DateTime=["+sConcat+"] iAA4_IndexOf=["+iAA4_IndexOf+"] iAA4_Length=["+iAA4_Length+"] Line Length=["+iLength+"]");
                sAA4 = p_sRecd.Substring(iAA4_IndexOf, iAA4_Length);
                sAA4_Fill1 = sAA4.Substring(1, 3);   // 3
                sAA4_Hours = sAA4.Substring(3, 2);   // 2
                sAA4_Pcp = sAA4.Substring(5, 4);   // 4
                sAA4_Trace = sAA4.Substring(9, 1);  // 1
                sAA4_Fill2 = sAA4.Substring(10, 1); // 1
                                                     //                        Console.WriteLine("AA4=["+sAA4+"] Pcp=["+sAA4_Pcp+"]");
                if (sAA4_Pcp.Equals("9999"))
                    sAA4_Pcp = "";
                else
                {
                    try
                    {
                        fWork = float.Parse(sAA4_Pcp);       // Convert to floating point
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] AA4_Pcp value could not be converted to floating point=[" + sAA4_Pcp + "]");
                        sAA4_Pcp = "";             // Data error.  Set to missing.
                    }
                    if (!sAA4_Pcp.Equals(""))
                        setPcp(fWork, sAA4_Hours, sAA4_Trace);
                }
            }
        }  // End of getAA4


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // setPcp - Take AA elements and set Precip values.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void setPcp(float fWork, string p_sHours, string p_sTrace)
        {
            // TODO - get rid of the fWork global variable (dangerous)
            if (convertToImperial)
                fWork = fWork / 10.0f * .03937008f;       // Convert precip depths from Millimeters to Inches
            else
                fWork /= 10.0f;

            if (p_sHours.Equals("01"))
            {
                sPcp01 = fWork.ToString(fmt5_2).Substring(0, 5);
                if (p_sTrace.Equals("2") && !sPcp01.Equals(""))
                {
                    // commenting out for now
                    //sPcp01t = "T";
                }
            }
            else
            {
                if (p_sHours.Equals("06"))
                {
                    sPcp06 = fWork.ToString(fmt5_2).Substring(0, 5);
                    if (p_sTrace.Equals("2") && !sPcp06.Equals(""))
                    {
                        // commenting out for now
                        //					sPcp06t = "T";
                    }
                }
                else
                {
                    if (p_sHours.Equals("24"))
                    {
                        sPcp24 = fWork.ToString(fmt5_2).Substring(0, 5);
                        if (p_sTrace.Equals("2") && !sPcp24.Equals(""))
                        {
                            // commenting out for now
                            //						sPcp24t = "T";
                        }
                    }
                    else
                    {
                        sPcp12 = fWork.ToString(fmt5_2).Substring(0, 5);
                        if (p_sTrace.Equals("2") && !sPcp12.Equals(""))
                        {
                            // commenting out for now
                            //						sPcp12t = "T";
                        }
                    }
                }
            }
        }  // End of setPcp


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAJ1 - Get AJ1 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAJ1(string p_sRecd)
        {
            sAJ1 = "";
            sAJ1_Fill1 = "";
            sAJ1_Sd = "";
            sAJ1_Fill2 = "";
            iAJ1_IndexOf = p_sRecd.IndexOf("AJ1");
            if ((iAJ1_IndexOf >= 0) && (iAJ1_IndexOf < iREM_IndexOf))
            {
                sAJ1 = p_sRecd.Substring(iAJ1_IndexOf, iAJ1_Length);
                sAJ1_Fill1 = sAJ1.Substring(1, 3);  // 3
                sAJ1_Sd = sAJ1.Substring(3, 4);  // 4
                sAJ1_Fill2 = sAJ1.Substring(7, 10); // 10
                                                    //                        Console.WriteLine("AJ1_Fill1=["+sAJ1_Fill1+"] Sd=["+sAJ1_Sd+"]");
                if (sAJ1_Sd.Equals("9999"))
                    sAJ1_Sd = "";
                else
                {
                    try
                    {
                        fWork = float.Parse(sAJ1_Sd);       // Convert to floating point
                    }
                    catch (Exception)
                    {
                        logIt(fDebug, iDEBUG, false, "sInFileName=[" + sInFileName + "] DateTime=[" + sConcat + "] sAJ1_Sd value could not be converted to floating point=[" + sAJ1_Sd + "]");
                        sAJ1_Sd = "";             // Data error.  Set to missing.
                    }
                    if (!sAJ1_Sd.Equals(""))
                    {
                        if (convertToImperial)
                            iWork = (int)(fWork * .3937008f + .5f);       // Convert precip depths from Millimeters to Inches
                        else
                            iWork = (int)(fWork);
                        sAJ1_Sd = iWork.ToString(fmt02).Substring(0, 2);
                    }
                }
            }
        }  // End of getAJ1


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAW1 - Get AW1 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAW1(string p_sRecd)
        {
            sAW1 = "";
            sAW1_Fill1 = "";
            sAW1_Zz = "";
            sAW1_Fill2 = "";
            iAW1_IndexOf = p_sRecd.IndexOf("AW1");
            if ((iAW1_IndexOf >= 0) && (iAW1_IndexOf < iREM_IndexOf))
            {
                sAW1 = p_sRecd.Substring(iAW1_IndexOf, iAW1_Length);
                sAW1_Fill1 = sAW1.Substring(1, 3);  // 3
                sAW1_Zz = sAW1.Substring(3, 2);  // 2
                sAW1_Fill2 = sAW1.Substring(5, 1);  // 1
                                                    //            logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sAW1_Zz=["+sAW1_Zz+"]");     // temporary - ras
            }
        }  // End of getAW1


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAW2 - Get AW2 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAW2(string p_sRecd)
        {
            sAW2 = "";
            sAW2_Fill1 = "";
            sAW2_Zz = "";
            sAW2_Fill2 = "";
            iAW2_IndexOf = p_sRecd.IndexOf("AW2");
            if ((iAW2_IndexOf >= 0) && (iAW2_IndexOf < iREM_IndexOf))
            {
                sAW2 = p_sRecd.Substring(iAW2_IndexOf, iAW2_Length);
                sAW2_Fill1 = sAW2.Substring(1, 3);  // 3
                sAW2_Zz = sAW2.Substring(3, 2);  // 2
                sAW2_Fill2 = sAW2.Substring(5, 1);  // 1
                                                    //            logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sAW2_Zz=["+sAW2_Zz+"]");     // temporary - ras
            }
        }  // End of getAW2


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAW3 - Get AW3 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAW3(string p_sRecd)
        {
            sAW3 = "";
            sAW3_Fill1 = "";
            sAW3_Zz = "";
            sAW3_Fill2 = "";
            iAW3_IndexOf = p_sRecd.IndexOf("AW3");
            if ((iAW3_IndexOf >= 0) && (iAW3_IndexOf < iREM_IndexOf))
            {
                sAW3 = p_sRecd.Substring(iAW3_IndexOf, iAW3_Length);
                sAW3_Fill1 = sAW3.Substring(1, 3);  // 3
                sAW3_Zz = sAW3.Substring(3, 2);  // 2
                sAW3_Fill2 = sAW3.Substring(5, 1);  // 1
                                                    //            logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sAW3_Zz=["+sAW3_Zz+"]");     // temporary - ras
            }
        }  // End of getAW3


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // getAW4 - Get AW4 element and format its output.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static void getAW4(string p_sRecd)
        {
            sAW4 = "";
            sAW4_Fill1 = "";
            sAW4_Zz = "";
            sAW4_Fill2 = "";
            iAW4_IndexOf = p_sRecd.IndexOf("AW4");
            if ((iAW4_IndexOf >= 0) && (iAW4_IndexOf < iREM_IndexOf))
            {
                sAW4 = p_sRecd.Substring(iAW4_IndexOf, iAW4_Length);
                sAW4_Fill1 = sAW4.Substring(1, 3);  // 3
                sAW4_Zz = sAW4.Substring(3, 2);  // 2
                sAW4_Fill2 = sAW4.Substring(5, 1);  // 1
                                                    //          logIt(fDebug, iDEBUG, false, "sInFileName=["+sInFileName+"] DateTime=["+sConcat+"] sAW4_Zz=["+sAW4_Zz+"]");     // temporary - ras
            }
        }  // End of getAW4


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // logIt - Append records to the log file.
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        public static int logIt(BinaryWriter p_fDebug, int p_iLogLevel, bool p_bFilter, string p_sIn)
        {
            int iRetCode = 99;                                        // Set default return code to something crazy.
            string sMessageFormatted = "";
            
            DateTime now = DateTime.Now;
            sMessageFormatted = sProgramName + ": " + now.ToString("yyyy-MM-dd HH:mm:ss") + "_" + p_sIn;

            if (bStdErr)
                Console.Error.WriteLine(sMessageFormatted);              // Error   mode will echo message to standard error

            if (bVerbose)
                Console.WriteLine(sMessageFormatted);              // Verbose mode will echo message to screen

            if (iLogLevel < p_iLogLevel)                            // 04/01/2009  ras
                return 0;                                           // No logging for this

            if (p_bFilter)                                          // 04/01/2009  ras
            {
                if (p_sFilter1.Equals("None")) { }
                else
                {
                    if (sConcat.Equals(p_sFilter1) || sConcat.Equals(p_sFilter2)) { }
                    else
                        return 0;
                }
            }

            try
            {
                p_fDebug = new BinaryWriter(new FileStream(sDebugName + ".debug", FileMode.Append));                // Append mode.
                p_fDebug.Write(now.ToString("yyyy-MM-dd HH:mm:ss") + "_" + p_sIn);           // Write output to debug log.
                iRetCode = 0;                                                                 // Good.
                p_fDebug.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine("5. Unable to open debug log");
                Console.Error.WriteLine(sProgramName + ": Stack trace follows:\n" + e.StackTrace);
                Environment.Exit(5);
            }
            catch (Exception e)
            {
                iRetCode = 6;                                                                 // An error occurred.
                Console.Error.WriteLine(sProgramName + ": Unspecified Exception in logIt. Error=[" + e.Message + "]");
                Console.Error.WriteLine(sProgramName + ": Stack trace follows:\n" + e.StackTrace);
                Environment.Exit(6);
            }
            return iRetCode;
        }  // End of logIt

    }
}
