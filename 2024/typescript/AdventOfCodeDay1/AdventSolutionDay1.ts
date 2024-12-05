import {readTextFile} from "../ReadInput";


export const Part1 = async () => {
    const start = performance.now()
    
    const input = await readTextFile("1")
    // const sample = await readTextSample()
    
    const arrayOne = [];
    const arrayTwo = [];
    
    const unorderedPairs = input.trim().split(/\r?\n/);
    
    unorderedPairs.forEach(pair => {
        const arrayedPair = pair.split(/\s+/);
        arrayOne.push(parseInt(arrayedPair[0]));
        arrayTwo.push(parseInt(arrayedPair[1]));
    })
    
    arrayOne.sort((a, b) => a - b);
    arrayTwo.sort((a, b) => a - b);
    
    let total = 0;
    
    for (let i = 0; i < arrayOne.length; i++) {
        total += Math.abs(arrayOne[i] - arrayTwo[i]);
    }

    const answer = total

    const end = performance.now();
    console.log(answer, " in ", (end-start).toFixed(2), " milliseconds");
}

export const Part2 = async () => {
    const start = performance.now()
    
    const input = await readTextFile("1")

    const set: Set<number> = new Set();
    const dictionary = new Map();

    const unorderedPairs = input.trim().split(/\r?\n/);

    unorderedPairs.forEach(pair => {
        const arrayedPair = pair.split(/\s+/);
        set.add(parseInt(arrayedPair[0]));
        dictionary.set(parseInt(arrayedPair[1]), (dictionary.get(parseInt(arrayedPair[1])) ?? 0) + 1);
    })

    let total = 0;

    set.forEach(value => {
        total += value * (dictionary.get(value) ?? 0);
    })
    
    const answer = total

    const end = performance.now();
    console.log(answer, " in ", (end-start).toFixed(2), " milliseconds");
}

export const Tests = async () => {
    
}