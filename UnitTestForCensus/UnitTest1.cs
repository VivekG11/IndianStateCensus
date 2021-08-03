using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using IndianStateCensus.POCO;
using IndianStateCensus;

namespace UnitTestForCensus
{
    [TestClass]
    public class UnitTest1
    {


            string stateCensusDataPath = @"C:\Users\VIVEK\source\repos\IndianStateCensus\IndianStateCensus\CSVFiles\StateCensusData.csv";
            string InvalidDataPath = @"C:\Users\VIVEK\source\repos\IndianStateCensus\IndianStateCensus\CSVFiles\InvalidData.txt";
            string WrongCensusDataPath = @"C:\Users\VIVEK\source\repos\IndianStateCensus\IndianStateCensus\CSVFiles\WrongDelimeterStateCensusData.csv";
            string InvalidHeaderPath = @"C:\Users\VIVEK\source\repos\IndianStateCensus\IndianStateCensus\CSVFiles\InavlidHeaderStatecensusData.csv";

            IndianStateCensus.CSVAdapterFactory csv = null;
            Dictionary<string, CensusDTO> totalRecords;
        

        [TestInitialize]

            public void SetUp()
            {
                csv = new IndianStateCensus.CSVAdapterFactory();
                totalRecords = new Dictionary<string, CensusDTO>();
            }

            [TestMethod]
        [TestCategory("Use Case 1")]
        public void GivenStateCensusShouldReturnRecords()
            {
                totalRecords = csv.LoadCsvData(CensusAnalyser.Country.INDIA, stateCensusDataPath, "State,Population,AreaInSqKm,Density");
                Assert.AreEqual(8, totalRecords.Count);

            }

        //TC 1.2
        [TestMethod]
        public void GivenIncorrectFileShouldThrowCustomException()
        {
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, InvalidDataPath, "State,Population,AreaInSqkm,DensityPerSqkm"));
            Assert.AreEqual(customException.exception, CensusAnalyserException.ExceptionType.FILE_NOT_FOUND);
        }

        [TestMethod]
     
        public void GivenWrongDelimeterReturnCustomException()
        {
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, WrongCensusDataPath, "State,Population,AreaInSqkm,DensityPerSqkm"));
            Assert.AreEqual(customException.exception, CensusAnalyserException.ExceptionType.INCOREECT_DELIMITER);
        }

        [TestMethod]
     
        public void GivenWrongHeaderReturnsCustomException()
        {
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, InvalidHeaderPath, "State,Population,AreaInSqkm,DensityPerSqkm"));
            Assert.AreEqual(customException.exception, CensusAnalyserException.ExceptionType.INCORRECT_HEADER);
        }


    }
}
