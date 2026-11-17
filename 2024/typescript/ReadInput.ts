import {promises as fs} from 'fs';
import path from "node:path";

export const createTextFiles = async () => {
    let count = -3;

    const items = await fs.readdir('.');

    for (const item of items) {
        const itemPath = path.join('.', item);
        const stats = await fs.stat(itemPath);

        if (stats.isDirectory()) {
            count++;
        }
    }
    
    const filePath = `AdventOfCodeDay${count}/AdventInputDay${count}.txt`
    let fileValue = ''
    try {
        fileValue = await fs.readFile(filePath, 'utf-8');
    } catch (err) {
        console.error('Error reading file:', err);
    }
    
    if (!fileValue) {
        await fs.writeFile(filePath, await fetch(`https://adventofcode.com/2024/day/${count}/input`,
            {
                headers:
                    {'Cookie': "session=53616c7465645f5f6ab8e7c5bfd0dc9887b5c7b9cdd323c69173c3aa2889cb438d322aeec1b0788bdae4c8ba23539d7d3d16c15d9870a1749471e3be4c88199d"}
            })
            .then(res => res.text())
        );
    }
}
export const readTextFile = async (day: string) => {
    const filePath = `AdventOfCodeDay${day}/AdventInputDay${day}.txt`
    try {
        return await fs.readFile(filePath, 'utf-8');
    } catch (err) {
        console.error('Error reading file:', err);
    }
}

export const readTextSample = async () => {
    const filePath = `sample.txt`
    try {
        return await fs.readFile(filePath, 'utf-8');
    } catch (err) {
        console.error('Error reading file:', err);
    }
}

createTextFiles().then(r => r)