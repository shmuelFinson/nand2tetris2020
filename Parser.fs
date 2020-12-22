
module Parser
open CodeWriter
open System
open System.IO


let parser(filePath:string,nameOfFile:string, futureFileName:string) = 
    let lines = File.ReadAllLines(filePath)
    let myFile = nameOfFile
    let commands = Seq.toList lines;
    let mutable counter = 2;
   // CodeWriter.initializeStack();
    for command in commands do  //we have a string per line of code
    let firstWord = command.Split([|" "|], StringSplitOptions.None)  //get the first word in the string (each line of code) to determine what kind of command we're talking about
    match firstWord.[0] with
         |"push" ->  CodeWriter.pushHackCommandSorter( command ,myFile)
         |"pop" -> CodeWriter.popHackCommandSorter (command, myFile)
         |"add" -> CodeWriter.add_2_hack command
         |"sub" -> CodeWriter.sub_2_hack command
         |"neg" -> CodeWriter.neg_2_hack command
         |"eq" -> CodeWriter.eq_2_hack counter ;counter <- counter + 3;
         |"lt" -> CodeWriter.lt_2_hack counter ; counter <- counter + 3;
         |"gt" -> CodeWriter.gt_2_hack counter ; counter <- counter + 3; 
         |"and" -> CodeWriter.and_2_hack command
         |"or" -> CodeWriter.or_2_hack command
         |"not" -> CodeWriter.not_2_hack command
         |"label" -> CodeWriter.label_2_hack(firstWord.[1],nameOfFile) // the value passed is the name of the label
         | "goto" ->  CodeWriter.goto(firstWord.[1],nameOfFile)
         | "if-goto" -> CodeWriter.if_goto_2_hack(firstWord.[1],nameOfFile)
         | "function" -> match firstWord.[1] with
                         |"Sys.init" -> CodeWriter.sys_init(firstWord.[1],nameOfFile)//CodeWriter.function_2_hack(firstWord.[1], Int32.Parse(firstWord.[2]), nameOfFile)
                         |_ -> CodeWriter.function_2_hack(firstWord.[1], Int32.Parse(firstWord.[2]), futureFileName)
         | "return" -> CodeWriter.return_2_hack()
         | "call" -> CodeWriter.call_2_hack(firstWord.[1] , Int32.Parse(firstWord.[2]),nameOfFile, counter)
         |_ ->  printf "%s" firstWord.[0]

