#!/usr/bin/env bash
# Usage: scripts/newday.sh N
# Scaffolds DayNN, DayNNTest, a sample file, and an empty puzzle input file.
set -euo pipefail
cd "$(dirname "$0")/.."

day=$((10#${1:?usage: newday.sh N}))
n=$(printf "%02d" "$day")
main="src/main/java/aoc/days/Day$n.java"
test="src/test/java/aoc/days/Day${n}Test.java"
sample="src/test/resources/samples/day$n.txt"
input="inputs/day$n.txt"
[ -e "$main" ] && { echo "Day$n already exists" >&2; exit 1; }
mkdir -p "$(dirname "$sample")" inputs

cat > "$main" <<JAVA
package aoc.days;

import aoc.Day;
import aoc.util.Input;

public class Day$n implements Day {
    @Override
    public String part1(String input) {
        return "TODO";
    }

    @Override
    public String part2(String input) {
        return "TODO";
    }
}
JAVA

cat > "$test" <<JAVA
package aoc.days;

import static org.junit.jupiter.api.Assertions.assertEquals;

import aoc.util.Samples;
import org.junit.jupiter.api.Test;

class Day${n}Test {
    // Fill these in with the answers from the puzzle text for $sample.
    private static final String PART1_SAMPLE_ANSWER = "?";
    private static final String PART2_SAMPLE_ANSWER = "?";

    private final Day$n day = new Day$n();
    private final String sample = Samples.read($day);

    @Test
    void part1Sample() {
        assertEquals(PART1_SAMPLE_ANSWER, day.part1(sample));
    }

    @Test
    void part2Sample() {
        assertEquals(PART2_SAMPLE_ANSWER, day.part2(sample));
    }
}
JAVA

touch "$sample" "$input"
echo "Created:"
printf '  %s\n' "$main" "$test" "$sample" "$input"
echo "Paste the example into $sample, then: ./gradlew test --tests Day${n}Test"
