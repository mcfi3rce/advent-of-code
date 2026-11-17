package aoc.days;

import aoc.Day;

public class Day01 implements Day {
    @Override
    public String part1(String input) {
        var level = 0;
        for (char parens : input.toCharArray()) {
            if (parens == '('){
                level++;
            }
            else{
                level--;
            }       
        }

        return String.valueOf(level);
    }

    @Override
    public String part2(String input) {
        var level = 0;
        var exit = 1;
        for (char parens : input.toCharArray()) {
            if (parens == '('){
                level++;
            }
            else{
                level--;
            }
            if (level < 0){
                return String.valueOf(exit);
            }       
            exit++;
        }
        return String.valueOf(exit);
    }
}
