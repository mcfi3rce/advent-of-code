import {readTextFile, readTextSample} from "../ReadInput";

const day = __filename[__filename.length - 4].toString()
export const Part1 = async () => {
    const start = performance.now();
    
    const input = await readTextFile(day)
    // const input = await readTextSample()
    
    const regex = /mul\(\d+,\d+\)/g;
    const matches = input.match(regex)
    
    let total = 0;
    matches.forEach(multiple => {
        let  numbers = multiple.substring(4, multiple.length - 1).split(',');
        total += parseInt(numbers[0]) * parseInt(numbers[1]);
    })
    
    const answer = total

    const end = performance.now();
    console.log(answer, " in ", (end-start).toFixed(2), " milliseconds");
}

export const Part2 = async () => {
    const start = performance.now();
    const input = await readTextFile(day)
    // const input = await readTextSample()
    

    const answer = trimString(input)

    const end = performance.now();
    console.log(answer, " in ", (end-start).toFixed(2), " milliseconds");
}

const trimString = (input: string) => {
    const regexDont = /don't\(\)/
    const resultDont = input.match(regexDont);

    const dontIndex = resultDont?.index ? resultDont.index+1 : undefined;
    
    const regexMatches = /mul\(\d+,\d+\)/g;
    const matches = input.substring(0,dontIndex).match(regexMatches)
        
    let total = 0;
    matches.forEach(multiple => {
        let  numbers = multiple.substring(4, multiple.length - 1).split(',');
        total += parseInt(numbers[0]) * parseInt(numbers[1]);
    })
    

    const regexDo = /do\(\)/
    const resultDo = input.substring(dontIndex).match(regexDo)
    
    const doIndex = resultDo?.index ? resultDo.index + 2 : -1 ;

    if (!resultDont?.index || doIndex == -1) return total;
    
    // console.log(`length: ${input.length}`)
    // console.log(`dontIndex: ${dontIndex}`);
    // console.log(`doIndex: ${doIndex}`)
    // console.log(`nextString: ${input.substring(dontIndex).substring(doIndex).length}`)
    
    return total + trimString(input.substring(dontIndex).substring(doIndex))
    
}

export const Tests = async () => {

}

//attempts: 66505565, 53783319