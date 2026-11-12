# Advent of Code 2015 (Java 17)

## Add a day
```
scripts/newday.sh 2
# 1. paste the puzzle's example into src/test/resources/samples/day02.txt
# 2. set PART1_SAMPLE_ANSWER / PART2_SAMPLE_ANSWER in Day02Test
# 3. implement Day02 until ./gradlew test --tests Day02Test passes
# 4. paste your real input into inputs/day02.txt and run it
```

## Run
```
./gradlew -q run --args="2"            # one day
./gradlew -q run                       # all days
./gradlew test --tests Day02Test       # one day's sample test
./gradlew test                         # all sample tests
```

Real puzzle inputs live in `inputs/` (gitignored). Samples live in `src/test/resources/samples/`.
