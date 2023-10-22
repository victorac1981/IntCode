using System.Collections.Generic;

namespace Parte1
{
    public class Process
    {
        private const int INITIAL_CHUNK_POSITION = -1;
        private const int SUMA_OPERATOR = 1;
        private const int MULTIPLY_OPERATOR = 2;
        private const int STOP_PROCESS = 99;

        private int startCurrentIndex;
        private string[] intCode;

        public Process(string contentFile)
        {
            intCode = contentFile.Split(',');
        }

        public string[] run()
        {
            if (isIntCodeNotEmpty())
            {
                startCurrentIndex = INITIAL_CHUNK_POSITION;
                bool isNotStopedProcess;
                do
                {
                    isNotStopedProcess = getChunkValuesAndProcess();
                    getNextChunk();
                } while (isNotEndOfValues() && isNotStopedProcess);
            }
            return intCode;
        }

        public bool getChunkValuesAndProcess()
        {
            Dictionary<string, int> chunk = new Dictionary<string, int>();
            chunk.Add("operator", getPosition(startCurrentIndex + 1));
            chunk.Add("firstOperand", getValueOfPosition(startCurrentIndex + 2));
            chunk.Add("secondOperand", getValueOfPosition(startCurrentIndex + 3));
            chunk.Add("positionToSave", getPosition(startCurrentIndex + 4));
            return processChunk(chunk);
        }

        private bool processChunk(Dictionary<string, int> chunk)
        {
            switch (chunk["operator"])
            {
                case STOP_PROCESS: return false;
                case SUMA_OPERATOR:
                    intCode[chunk["positionToSave"]] =
                        (chunk["firstOperand"] + chunk["secondOperand"]).ToString();
                    break;
                case MULTIPLY_OPERATOR:
                    intCode[chunk["positionToSave"]] =
                       (chunk["firstOperand"] * chunk["secondOperand"]).ToString();
                    break;
                default: return true;
            }
            return true;
        }


        private bool isIntCodeNotEmpty()
        {
            return intCode.Length != 0;
        }

        private void getNextChunk()
        {
            startCurrentIndex += 4;
        }

        private bool isNotEndOfValues()
        {
            return startCurrentIndex + 1 < intCode.Length;
        }

        private int getPosition(int index)
        {
            return int.Parse(intCode[index]);
        }

        private int getValueOfPosition(int index)
        {
            return int.Parse(intCode[getPosition(index)]);
        }
    }
}