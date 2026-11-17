import {readTextFile, readTextSample} from "../ReadInput";


export const Part1 = async () => {
    const start = performance.now();
    
    const input = await readTextFile("2")
    // const input = await readTextSample()
    
    const reports = input.trim().split("\n");
    
    const answer = "Pending..."
    
    const end = performance.now();
    console.log(answer, " in ", (end-start).toFixed(2), " milliseconds");
}

export const Part2 = async () => {
    const start = performance.now();
    
    const input = await readTextFile("2")
    // const input = await readTextSample()
    
    const reports = input.trim().split("\n");

    const answer = "Pending..."
    
    const end = performance.now();
    console.log(answer, " in ", (end-start).toFixed(2), " milliseconds");
}

export const Tests = async () => {

}