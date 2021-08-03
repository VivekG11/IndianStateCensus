using System;
using IndianStateCensus.POCO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace IndianStateCensus
{
    public class IndianCensusAdapter : CensusAdapter
    {

        string[] censusData;
        Dictionary<string, CensusDTO> datamap;
        public Dictionary<string, CensusDTO> LoadCensusData(string csvFilePath, string dataHeaders)
        {
            try
            {
                datamap = new Dictionary<string, CensusDTO>();
                censusData = GetCensusData(csvFilePath, dataHeaders);
                foreach (string data in censusData.Skip(1))
                {
                    if (!data.Contains(","))
                    {
                        throw new CensusAnalyserException("File Containers Wrong Delimiter", CensusAnalyserException.ExceptionType.INCOREECT_DELIMITER);//
                    }
                    string[] coloumn = data.Split(',');
                    if (csvFilePath.Contains("IndianStateCode.csv"))
                    {
                        datamap.Add(coloumn[1], new CensusDTO(new StateCodeDAO(coloumn[0], coloumn[1], coloumn[2], coloumn[3])));
                    }
                    if (csvFilePath.Contains("IndiaStateCensusData.csv"))
                    {
                        datamap.Add(coloumn[1], new CensusDTO(new CensusDataDAO(coloumn[0], coloumn[1], coloumn[2], coloumn[3])));
                    }
                }
                return datamap;
            }
            catch (CensusAnalyserException ex)
            {
                throw ex;
            }
        }
    }
}
