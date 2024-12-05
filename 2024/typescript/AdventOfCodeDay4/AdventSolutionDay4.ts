import {readTextFile, readTextSample} from "../ReadInput";

const day = __filename[__filename.length - 4].toString()

export const Part1 = async () => {
    const start = performance.now();

    const input = await readTextFile(day)
    // const input = await readTextSample()
    
    const rows = input.trim().split("\n");
    
    const allRows = [...rows]
    
    let count = 0;
    count += CountMatches(rows);
    const flippedRows = [];
    
    for (let column = 0; column < rows[0].length; column++) {
        allRows.push(rows.map(str => str[str.length - (1+column)]).join(""));
    }

    count += CountMatches(flippedRows);
    
    for (let counterIndex = 0; counterIndex < rows.length; counterIndex++) {
        let columnIndex = rows[0].length-1 - counterIndex;
        let rowIndex = 0;

        const currentStringDiamond = [];
        const currentStringReverseDiamond = [];
        while(columnIndex < rows[0].length && rowIndex <= counterIndex) {
            currentStringDiamond.push(rows[rowIndex][columnIndex]);
            currentStringReverseDiamond.push(rows[rows.length - rowIndex-1][columnIndex]);
            columnIndex++;
            rowIndex++;
        }

        allRows.push(currentStringDiamond.join(""));
        allRows.push(currentStringReverseDiamond.join(""));
    }
    
    for (let counterIndex = 0; counterIndex < rows.length-1; counterIndex++) {
        let columnIndex = counterIndex;
        let rowIndex = 0;

        const currentStringDiamond = [];
        const currentStringReverseDiamond = [];
        while(columnIndex >= 0 && rowIndex <= counterIndex) {
            currentStringReverseDiamond.push(rows[rowIndex][columnIndex]);
            currentStringDiamond.push(rows[rows.length - rowIndex-1][columnIndex]);
            columnIndex--;
            rowIndex++;
        }

        allRows.push(currentStringDiamond.join(""));
        allRows.push(currentStringReverseDiamond.join(""));
    }

    const answer = CountMatches(allRows)

    const end = performance.now();
    console.log(answer, " in ", (end-start).toFixed(2), " milliseconds");
}

export const Part2 = async () => {
    const start = performance.now();
    
    const input = await readTextFile(day)
    // const input = await readTextSample()

    const rows = input.trim().split("\n");
    
    let count = 0;
    for (let row = 1; row < rows.length-1; row++) {
        for (let column = 1; column < rows[0].length-1; column++) {   
            if (rows[row][column] == 'A') {
                let firstCross = [rows[row-1][column-1], rows[row+1][column+1]].join('');
                let secondCross = [rows[row-1][column+1], rows[row+1][column-1]].join('');
                if ((firstCross == "SM" || firstCross == "MS") && (secondCross == "SM" || secondCross == "MS")) count++;
            }
        }
    }
    
    const answer = count

    const end = performance.now();
    console.log(answer, " in ", (end-start).toFixed(2), " milliseconds");
}

const CountMatches = (rows: string[]) => {
    const fRegex = /XMAS/g;
    const rRegex = /SAMX/g;
    
    let count = 0;
    rows.forEach((row) => {
        count += Array.from(row.matchAll(fRegex)).length;
        count += Array.from(row.matchAll(rRegex)).length;
    })
    return count;
}


export const Tests = async () => {

}