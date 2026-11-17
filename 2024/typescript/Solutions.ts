import {promises as fs} from 'fs';
import * as path from "node:path";

const importedDays = [
    "./AdventOfCodeDay1/AdventSolutionDay1",
    "./AdventOfCodeDay2/AdventSolutionDay2",
    "./AdventOfCodeDay3/AdventSolutionDay3",
    "./AdventOfCodeDay4/AdventSolutionDay4",
    "./AdventOfCodeDay5/AdventSolutionDay5",
    "./AdventOfCodeDay6/AdventSolutionDay6",
    "./AdventOfCodeDay7/AdventSolutionDay7",
    "./AdventOfCodeDay8/AdventSolutionDay8",
    "./AdventOfCodeDay9/AdventSolutionDay9",
    "./AdventOfCodeDay10/AdventSolutionDay10",
    "./AdventOfCodeDay11/AdventSolutionDay11",
    "./AdventOfCodeDay12/AdventSolutionDay12",
    "./AdventOfCodeDay13/AdventSolutionDay13",
    "./AdventOfCodeDay14/AdventSolutionDay14",
    "./AdventOfCodeDay15/AdventSolutionDay15",
    "./AdventOfCodeDay16/AdventSolutionDay16",
    "./AdventOfCodeDay17/AdventSolutionDay17",
    "./AdventOfCodeDay18/AdventSolutionDay18",
    "./AdventOfCodeDay19/AdventSolutionDay19",
    "./AdventOfCodeDay20/AdventSolutionDay20",
    "./AdventOfCodeDay21/AdventSolutionDay21",
    "./AdventOfCodeDay22/AdventSolutionDay22",
    "./AdventOfCodeDay23/AdventSolutionDay23",
    "./AdventOfCodeDay24/AdventSolutionDay24",
    "./AdventOfCodeDay25/AdventSolutionDay25"
]

async function loadModules(count: number) {
    // Now, `importedModules` will be an array of the imported modules
    return await Promise.all(
        importedDays.slice(0, count).map((modulePath) => import(modulePath))
    );
}

const main = async () => {

    let count = -3;
    
    const items = await fs.readdir('.');

    for (const item of items) {
        const itemPath = path.join('.', item);
        const stats = await fs.stat(itemPath);

        if (stats.isDirectory()) {
            count++; 
        }
    }

    
    
    const days = await loadModules(count);

    let num = 1;

    for (const day of days) {
        console.log(`===== Day Number ${num} solutions in progress =====`);
        await day.Part1();
        await day.Part2();
        await day.Tests();
        
        num++;
    }
}

main()