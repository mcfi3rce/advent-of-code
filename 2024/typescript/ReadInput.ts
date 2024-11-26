import { promises as fs } from 'fs';

export const readTextFile = async (day: string) => {
    const filePath = `AdventOfCodeDay${day}/AdventInputDay${day}.txt`
    try {
        const data = await fs.readFile(filePath, 'utf-8');
        return data;
    } catch (err) {
        console.error('Error reading file:', err);
    }
}
