
open Parser
open System
open System.IO

[<EntryPoint>]
let main argv =
   

      //give the name of the directory to be parsed...
     //  let directory = """C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\08\FunctionCalls\StaticsTest\"""
      // let directoryArray = Directory.GetFiles(directory, "*.vm") 


      //lab 1:
      //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\07\StackArithmetic\SimpleAdd
      //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\07\StackArithmetic\StackTest\StackTest.vm
      //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\07\MemoryAccess\StaticTest.vm
      //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\07\MemoryAccess\PointerTest\PointerTest.vm
      //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\07\MemoryAccess\BasicTest\BasicTest.vm

      // lab 2:
   //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\08\ProgramFlow\BasicLoop\BasicLoop.vm
   //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\08\ProgramFlow\FibonacciSeries\FibonacciSeries.vm
   //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\08\FunctionCalls\SimpleFunction\SimpleFunction.vm
   //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\08\FunctionCalls\NestedCall\Sys.vm
   //C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\08\FunctionCalls\FibonacciElement\Main.vm
   //hello
                
       let directory  = """C:\Users\Shmuel Finson\Desktop\עקרונות שפות תכנה\nand2tetris\projects\08\FunctionCalls\StaticsTest"""
       let futurefileName = "StaticsTest"
       //this function only writes Sys.init into the file
       CodeWriter.initializeStack()
       //run hack code for Sys.init function, which is always inside Sys.vm
       let writeSysInit = 
        for file in Directory.GetFiles(directory,"*.vm") do
            if String.Equals(Path.GetFileName(file),"Sys.vm") then
              let lines = File.ReadAllLines(file)
              let commands = Seq.toList lines;
              for command in commands do  //we have a string per line of code
                 let firstWord = command.Split([|" "|], StringSplitOptions.None)  //get the first word in the string (each line of code) to determine what kind of command we're talking about
                 match firstWord.[0] with
                 |"function" -> match firstWord.[1] with
                                |"Sys.init" -> CodeWriter.sys_init(firstWord.[1],futurefileName)
                                |_ -> printf "Not\n"
                 |_-> printf ""

        //find all other .vm files in directory, and write hack code for them
       let writeOtherFilesExceptSys = 
         for file in Directory.GetFiles(directory,"*.vm") do
            if not(String.Equals(Path.GetFileName(file),"Sys.vm")) then
                let nof = Path.GetFileName(file).Substring(0,Path.GetFileName(file).Length - 3)
                Parser.parser(file, nof, futurefileName)
       
       // go back to Sys.vm and run the hack translator on it
       let writeEndOfSysLabel = 
        for file in Directory.GetFiles(directory,"*.vm") do
            if String.Equals(Path.GetFileName(file),"Sys.vm") then
              let lines = File.ReadAllLines(file)
              let commands = Seq.toList lines;
              for command in commands do  //we have a string per line of code
                 let firstWord = command.Split([|" "|], StringSplitOptions.None)  //get the first word in the string (each line of code) to determine what kind of command we're talking about
                 match firstWord.[0] with
                 |"function" -> match firstWord.[1] with
                                |"Sys.init" -> CodeWriter.function_2_hack(firstWord.[1], Int32.Parse(firstWord.[2]), futurefileName)
                                |_ -> printf ""
                 |_-> printf ""

        //finish running all the Sys.vm code that is left, if we had to leave the file.
       let writeEndOfSysFile = 
        for file in Directory.GetFiles(directory,"*.vm") do
            if String.Equals(Path.GetFileName(file),"Sys.vm") then
              let lines = File.ReadAllLines(file)
              let commands = Seq.toList lines;
              let mutable counter = 2;
              for command in commands do  //we have a string per line of code
                 let firstWord = command.Split([|" "|], StringSplitOptions.None) 
                 if not(String.Equals(firstWord.[0],"function") && String.Equals(firstWord.[1],"Sys.init")) then
                    match firstWord.[0] with
                            |"push" ->  CodeWriter.pushHackCommandSorter( command ,futurefileName)
                            |"pop" -> CodeWriter.popHackCommandSorter (command, futurefileName)
                            |"add" -> CodeWriter.add_2_hack command
                            |"sub" -> CodeWriter.sub_2_hack command
                            |"neg" -> CodeWriter.neg_2_hack command
                            |"eq" -> CodeWriter.eq_2_hack counter ;counter <- counter + 3;
                            |"lt" -> CodeWriter.lt_2_hack counter ; counter <- counter + 3;
                            |"gt" -> CodeWriter.gt_2_hack counter ; counter <- counter + 3; 
                            |"and" -> CodeWriter.and_2_hack command
                            |"or" -> CodeWriter.or_2_hack command
                            |"not" -> CodeWriter.not_2_hack command
                            |"label" -> CodeWriter.label_2_hack(firstWord.[1],futurefileName) // the value passed is the name of the label
                            | "goto" ->  CodeWriter.goto(firstWord.[1],futurefileName)
                            | "if-goto" -> CodeWriter.if_goto_2_hack(firstWord.[1],futurefileName)
                            | "function" -> match firstWord.[1] with
                                            |"Sys.init" -> CodeWriter.sys_init(firstWord.[1],futurefileName)//CodeWriter.function_2_hack(firstWord.[1], Int32.Parse(firstWord.[2]), nameOfFile)
                                            |_ -> CodeWriter.function_2_hack(firstWord.[1], Int32.Parse(firstWord.[2]), futurefileName)
                            | "return" -> CodeWriter.return_2_hack()
                            | "call" -> CodeWriter.call_2_hack(firstWord.[1] , Int32.Parse(firstWord.[2]),futurefileName, counter)
                            |_ ->  printf "%s" firstWord.[0]


       //let lines = File.ReadAllLines(path)
     //// let myFile = "StaticsTest"
     //  let commands = Seq.toList lines;
    //   let results =
           //   Parser.parser(file, fileName)      Parser.parser(commands,myFile);
         
     
       
       0 // return an integer exit code

        

       





  