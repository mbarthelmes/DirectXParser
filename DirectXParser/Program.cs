// See https://aka.ms/new-console-template for more information
using System.Collections;

class DirectXParser
{
    static void Main()
    {
        String fileName = @"C:\Users\matth\Documents\CS470\Models\cone.obj";
        String destinationFile = @"C:\Users\matth\Documents\CS470\Models\cone.txt";

        String vertexCount = "Vertex Count: 0";

        var vertex = new ArrayList();
        var texture = new ArrayList();
        var normal = new ArrayList();
        var fVert = new ArrayList();

        int vertexCounter = 0;
        int textureCounter = 0;
        int normalCounter = 0;
        int fVertCounter = 0;


        Stream s = new FileStream(fileName, FileMode.Open);
        int val = 0;
        char ch;
        String toArray = "";
        String fNumber = "";
        Boolean isV = false;
        Boolean isVT = false;
        Boolean isVN = false;
        Boolean isF = false;

        int fType = 0;

        while (true)
        {
            val = s.ReadByte();

            if (val < 0)
            {
                break;
            }

            ch = (char)val;

            if (isV == true)
            {
                if (ch == '\n')
                {
                    vertex.Add(toArray);
                    isV = false;
                    toArray = "";
                    vertexCounter++;
                }
                else
                {
                    toArray += ch;
                }
            }
            else if (isVT == true)
            {
                if (ch == '\n')
                {
                    texture.Add(toArray);
                    isVT = false;
                    toArray = "";
                    textureCounter++;
                }
                else
                {
                    toArray += ch;
                }
            }
            else if (isVN == true)
            {
                if (ch == '\n')
                {
                    normal.Add(toArray);
                    isVN = false;
                    toArray = "";
                    normalCounter++;
                }
                else
                {
                    toArray += ch;
                }
            }
            else if (isF == true)
            {
                if (ch == '\n')
                {
                    int arrayPoint = Int32.Parse(fNumber);
                    fVert.Add(normal[arrayPoint-1]);
                    fNumber = "";
                    isF = false;
                    fVertCounter++;
                    fType = 0;
                }
                else if (ch == ' ')
                {
                    int arrayPoint = Int32.Parse(fNumber);
                    fVert.Add(normal[arrayPoint - 1]);
                    fNumber = "";
                    fVertCounter++;
                    fType = 0;
                }
                else if (ch == '/')
                {
                    int arrayPoint = Int32.Parse(fNumber);
                    if(fType == 0)
                    {
                        fVert.Add(vertex[arrayPoint-1]);
                    }
                    else
                    {
                        fVert.Add(texture[arrayPoint - 1]);
                    }
                    fType++;
                    fNumber = "";
                }
                else
                {
                    Boolean checkNum = true;
                    fNumber += ch;
                }
            }

            if (isV == false & isVT == false & isVN == false & isF == false)
            {
                if (ch.Equals('v'))
                {
                    val = s.ReadByte();
                    ch = (char)val;

                    if(ch == ' ')
                    {
                        isV = true;
                    }
                    else if (ch == 't')
                    {
                        val = s.ReadByte();
                        isVT = true;
                    }
                    else if (ch == 'n')
                    {
                        val = s.ReadByte();
                        isVN = true;
                    }
                    else
                    {
                        //Console.WriteLine("File wasn't formatted correctly.");
                    }
                }
                else if (ch == 'f')
                {
                    val = s.ReadByte();
                    ch = (char)val;
                    if (ch == ' ')
                    {
                        isF = true;
                    }
                }
                else
                {
                    //Console.WriteLine("File wasn't formatted correctly.");
                }
            }
            


            //ch = (char)val;
            //Console.Write(ch);
        }
        for(int i = 0; i < fVert.Count; i++)
        {
            Console.Write(fVert[i]);
            Console.Write(" ");
            i++;
            Console.Write(fVert[i]);
            Console.Write(" ");
            i++;
            Console.WriteLine(fVert[i]);
        }
        Console.WriteLine();

        vertexCount = "Vertex Count: " + fVertCounter;

        // Write the string array to a new file named "WriteLines.txt".
        using (StreamWriter outputFile = new StreamWriter(destinationFile))
        {
            fType = 0;

            outputFile.WriteLine(vertexCount);
            outputFile.WriteLine("");
            outputFile.WriteLine("Data:");
            outputFile.WriteLine("");

            foreach (string line in fVert)
            {
                if(fType < 2)
                {
                    outputFile.Write(line);
                    outputFile.Write(" ");
                    fType++;
                }
                else
                {
                    outputFile.WriteLine(line);
                    fType = 0;
                }
            }
        }

        Console.WriteLine("File Created");
    }
}