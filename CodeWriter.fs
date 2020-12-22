
module CodeWriter


open System
open System.IO


//pop functions:

  //pop segment(local/argument/this/that) offset  - pop #1
  let pop_segment_local_argument_this_that_offset(register:string, offset:string) =  
      let offset1 = offset
      let reg = match register with
      |"local" ->  "LCL"
      |"argument" ->  "ARG"
      |"this" ->  "THIS"
      |"that" ->  "THAT"

      let newLine = "\n"
      let comment = "//pop " + reg +  offset1 + newLine 
      let atOffset = "@"+ offset1 + "\n" 
      let atRegister = "@" + reg + "\n"
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n")
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",comment)                                                                                                                                                                           
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@SP\n")
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","M=M-1\n")
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",atOffset)
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","D=A\n")
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",atRegister)
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D=M+D
@R13
M=D
@SP
A=M
D=M
@R13
A=M
M=D\n")

let pop_segement_pointer_temp(register:string, offset:string) = 
    let comment = "//pop " + register + offset
    let reg = match register with
           |"pointer" ->  "3"
           |"temp" ->  "5"
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",comment + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + offset)
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","D=A\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + reg + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D=A+D
@R13
M=D
@SP
A=M
D=M
@R13
A=M
M=D\n")









  //pop static offset - pop #3
  let pop_static_offset(currentFileName:string, offset:string, nameOfFile:string) = 
    let comment = "//pop static " + offset
    let atFileName = "@" + nameOfFile + ".vm." + offset + "\n"
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1
A=M
D=M\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",atFileName)
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","M=D\n")



   

  //pop constant offset - pop #4
  let pop_constant_offset(offset:string) = 
      let offset1 = offset
      let newLine = "\n"
      let comment = "//pop constant " + offset1 + newLine
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n")
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",comment)
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1\n")



  // push functions:

  //push segment(local/argument/this/that) offset  - push #1
  let push_Lcl_Arg_This_That_Offset (register:string , offset:string) = 
      let sourceReg = register
      let offset1 = offset.ToString()
      let newLine = "\n"
      let reg = match register with
          |"local" ->  "LCL"
          |"argument" ->  "ARG"
          |"this" ->  "THIS"
          |"that" ->  "THAT"

      let comment = "//push " + reg + " " + offset1 + newLine
      let atReg = "@" + offset1
      let atSourceReg = "@" + sourceReg + newLine
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n")
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",comment)
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",atReg)
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"\n
D=A\n")
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + reg + "\n") //"@%s\n" register
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1\n")

  //push (temp) offset  - push #2





let push_pointer_temp_offset(register:string,offset:string) = 
   let atOffset = "@" + offset
   let comment = "//push " + register + offset
   let reg = match register with
        |"pointer" ->  "3"
        |"temp" ->  "5"
   File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",comment + "\n")
   File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",atOffset + "\n")
   File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D=A\n")
   File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + reg + "\n")
   File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"A=A+D
D=M
@SP
A=M
M=D
@SP
M=M+1\n")









  //push static offset - push #3
  let push_static_offset(value:string, fileName:string) =
      let offset = value
      let newLine = "\n"
      let comment = "// push static " + offset + newLine
      let atFileName = "@"+ fileName + ".vm." + offset + "\n"
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n")
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",comment)
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",atFileName)
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D=M
@SP
A=M
M=D
@SP
M=M+1\n")


  
  //push constant value - push #4
  let push_constant_value(value:string)=
      let offset = value
      let newLine = "\n"
      let comment = "// push constant " + offset + newLine
      let atOffset = "@" + offset + newLine
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n")
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",comment)
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm",atOffset)
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D=A
@SP
A=M
M=D
@SP
M=M+1\n")

      





  //there are several different types of push commands, depending on the register - this function calls the appropriate push function...
  let pushHackCommandSorter (lineOfCode:string, nameOfFile:string) = 
      let hackCode = lineOfCode.Split([|" "|], StringSplitOptions.None) //push - 0 constant - 1 2 - 2
      let sourceReg = hackCode.[1] //name of register 
      let offset = hackCode.[2] //address in register
      //determining the correct push command:
      match sourceReg with
                  |"local" -> push_Lcl_Arg_This_That_Offset (sourceReg, offset)
                  |"argument" -> push_Lcl_Arg_This_That_Offset (sourceReg, offset)
                  |"this" -> push_Lcl_Arg_This_That_Offset (sourceReg, offset)
                  |"that" -> push_Lcl_Arg_This_That_Offset (sourceReg, offset)
                  |"constant" -> push_constant_value(offset)
                  | "static" -> push_static_offset(offset, nameOfFile)
                  |"temp" -> push_pointer_temp_offset(sourceReg, offset)
                  |"pointer" -> push_pointer_temp_offset(sourceReg, offset)
                

     


 //there are several different types of pop commands, depending on the register - this function calls the appropriate pop function...
  let popHackCommandSorter (lineOfCode:string, nameOfFile:string) = 
      let hackCode = lineOfCode.Split([|" "|], StringSplitOptions.None)
      let sourceReg = hackCode.[1]
      let offset = hackCode.[2]
      //determining the correct pop command:
      match sourceReg with
                  |"local" -> pop_segment_local_argument_this_that_offset (sourceReg, offset)
                  |"argument" -> pop_segment_local_argument_this_that_offset (sourceReg, offset)
                  |"this" -> pop_segment_local_argument_this_that_offset (sourceReg, offset)
                  |"that" -> pop_segment_local_argument_this_that_offset (sourceReg, offset)
                  |"constant" -> pop_constant_offset(offset)
                  |"temp"-> pop_segement_pointer_temp(sourceReg,offset)
                  |"pointer" ->pop_segement_pointer_temp(sourceReg,offset)
                  | "static" -> pop_static_offset(sourceReg,offset, nameOfFile)
                  |_ ->  printf "Unknown\n"

  //translate an add command into HACK
  let add_2_hack (lineOfCode:string) = 
     File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm" 
,"\n
//add
@SP
M=M-1
A=M
D=M
@SP
M=M-1
@SP
A=M
M=M+D
@SP
M=M+1\n")



  //translate a sub command into HACK
  let sub_2_hack (lineOfCode:string) = 
       File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"\n
//sub
@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M
M=M-D
@SP
M=M+1\n")

//translate neg to hack
  let neg_2_hack(lineOfCode:string) = 
       File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"\n//neg
@SP
M=M-1
A=M
M=-M
@SP
M=M+1\n")

//translate eq to hack
  let eq_2_hack(eq_lab_num: int) = 
    let num = eq_lab_num.ToString();
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//eq\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M

D=M-D\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@inequal" + num + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D;JNE
@SP
A=M
M=-1\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@end" + num + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"0;JMP\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(inequal" + num + ")" + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
A=M
M=0\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(end" + num + ")" + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M+1\n")

//translate gt to hack
let gt_2_hack(gt_lab_num: int) = 
    let num = gt_lab_num.ToString();
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//gt\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M

D=M-D\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@greater" + num + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D;JGT
@SP
A=M
M=0\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@end" + num + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","0;JMP\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(greater" + num + ")" + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
A=M
M=-1\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(end"  + num + ")" + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M+1\n")


//translate lt to hack
let lt_2_hack(lt_lab_num: int) = 
    let num = lt_lab_num.ToString();
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//lt\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","D=M-D\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@greater" + num + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D;JGE
@SP
A=M
M=-1\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@end" + num + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","0;JMP\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(greater" + num + ")" + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
A=M
M=0\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(end"  + num + ")" + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M+1\n")



//translate and to hack
let and_2_hack(lineOfCode:string) = 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//and\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1
A=M
D=M
@SP
M=M-1
@SP
A=M
M=M&D
@SP
M=M+1\n")

//translate or to hack
let or_2_hack(lineOfCode:string) = 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//or\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1
A=M
D=M
@SP
M=M-1
@SP
A=M
M=M|D
@SP
M=M+1\n")

//transle not to hack
let not_2_hack(lineOfCode:string) = 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//not\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1
A=M
M=!M
@SP
M=M+1\n")

//Creates a label
let label_2_hack(labelName: string, nof:string) = 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//label " + labelName + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(" + nof + "." + "$" + labelName + ")\n" )

let if_goto_2_hack(labelName:string,nof:string) = 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//if-goto " + labelName + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@SP
M=M-1
A=M
D=M\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + nof + "." + "$" + labelName + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D;JNE\n")

let goto(labelName:string,nof:string) = 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//goto " + labelName + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + nof + "." + "$"+ labelName + "\n") 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","0;JMP\n")

let function_2_hack(funcName:string, counter:int, nof:string) = 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//function " + funcName + counter.ToString() + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(" + nof + "." + funcName + ")\n") 
    for i = 1 to counter do
      File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@0
D=A
@SP
A=M
M=D
@SP
M=M+1\n");
    

let return_2_hack() = 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","\n//return\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@LCL 
D=M
@endFrame
M=D
@5    
D=A
@endFrame
A=M-D
D=M
@retAddr
M=D
@SP   
M=M-1
A=M
D=M
@ARG  
A=M
M=D
@ARG  
D=M+1
@SP
M=D
@endFrame
A=M-1
D=M
@THAT
M=D
@2
D=A
@endFrame
A=M-D
D=M
@THIS
M=D
@3
D=A
@endFrame
A=M-D
D=M
@ARG
M=D
@4
D=A
@endFrame
A=M-D
D=M
@LCL
M=D
@retAddr
A=M
0;JMP\n")

let initializeStack() = 
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","//initialize Stack\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"@256
D=A
@0
M=D\n")

let call_2_hack(functionName:string, numOfArgs:int, nof:string, counter:int) = 
    let nums = new System.Random();
    let num = nums.Next(-1,100);  
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","//call "+ functionName + numOfArgs.ToString() + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + nof  + "." + functionName + "$ret." + num.ToString() + "\n")//return addr
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D=A
@SP
A=M
M=D
@SP
M=M+1
// store caller's lcl
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// store caller's arg
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
//store caller's this
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// store caller's that
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// change arg to callee's arg\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + numOfArgs.ToString() + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D=A
@5
D=D+A
@SP
D=M-D
@ARG
M=D
// change lcl to callee's lcl
@SP
D=M
@LCL
M=D
// goto - function call\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + nof + "." + functionName + "\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"0;JMP\n")
    File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(" + nof + "." + functionName + "$ret." + num.ToString() + ")" + "\n");

let sys_init(functionName:string,nof:string) = 
        let nums = new System.Random();
        let num = nums.Next(-1,30);  
        File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","//call "+ functionName + "\n")
        File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + nof  + "." + functionName + "$ret." + num.ToString() + "\n")//return addr
        File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D=A
@SP
A=M
M=D
@SP
M=M+1
// store caller's lcl
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// store caller's arg
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
//store caller's this
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// store caller's that
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// change arg to callee's arg\n")
        File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@0\n")
        File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"D=A
@5
D=D+A
@SP
D=M-D
@ARG
M=D
// change lcl to callee's lcl
@SP
D=M
@LCL
M=D
// goto - function call\n")
        File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","@" + nof + "." + functionName + "\n")
        File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm"
,"0;JMP\n")
        File.AppendAllText("C:\Users\Shmuel Finson\Desktop\experiment.asm","(" + nof + "." + functionName + "$ret." + num.ToString() + ")" + "\n")
        







