package main

import (
	"fmt"
	"os"
	"path/filepath"
	"strconv"
	"strings"
)

func main() {
	if len(os.Args) != 2 {
		fmt.Println("Usage: go run create-day.go <day-number>")
		os.Exit(1)
	}

	day, err := strconv.Atoi(os.Args[1])
	if err != nil || day < 1 || day > 25 {
		fmt.Println("Error: Day must be a number between 1 and 25")
		os.Exit(1)
	}

	dayPadded := fmt.Sprintf("%02d", day)
	dayFolder := filepath.Join("AdventOfCode24", "Days", fmt.Sprintf("Day%s", dayPadded))
	solutionClass := fmt.Sprintf("Day%s", dayPadded)

	// Create day folder
	if err := os.MkdirAll(dayFolder, 0755); err != nil {
		fmt.Printf("Error creating folder %s: %v\n", dayFolder, err)
		os.Exit(1)
	}
	fmt.Printf("✅ Created folder: %s\n", dayFolder)

	// Create solution file
	solutionContent := fmt.Sprintf(`using System;
using System.Linq;

namespace AdventOfCode24.Days.Day%s
{
    public class %s : ISolution
    {
        public string Part1(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            // TODO: Implement Part 1
            return "0";
        }

        public string Part2(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            // TODO: Implement Part 2
            return "0";
        }
    }
}`, dayPadded, solutionClass)

	solutionFile := filepath.Join(dayFolder, fmt.Sprintf("%s.cs", solutionClass))
	if !fileExists(solutionFile) {
		if err := writeFile(solutionFile, solutionContent); err != nil {
			fmt.Printf("Error creating solution file: %v\n", err)
			os.Exit(1)
		}
		fmt.Printf("✅ Created solution: %s\n", solutionFile)
	}

	// Create test file
	testContent := fmt.Sprintf(`using AdventOfCode24.Days.Day%s;

namespace AdventOfCode24.Tests;

public class %sTests
{
    private readonly %s _solution = new();

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay%sSample();
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay%sInput();
        var result = _solution.Part1(input);
        // Add expected result once you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay%sSample();
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay%sInput();
        var result = _solution.Part2(input);
        // Add expected result once you know it
        Assert.Equal("0", result);
    }
}`, dayPadded, solutionClass, solutionClass, dayPadded, dayPadded, dayPadded, dayPadded)

	testFile := filepath.Join("AdventOfCode24.Tests", fmt.Sprintf("%sTests.cs", solutionClass))
	if !fileExists(testFile) {
		if err := writeFile(testFile, testContent); err != nil {
			fmt.Printf("Error creating test file: %v\n", err)
			os.Exit(1)
		}
		fmt.Printf("✅ Created test: %s\n", testFile)
	}

	// Create input files
	sampleFile := filepath.Join(dayFolder, "sample.txt")
	inputFile := filepath.Join(dayFolder, "input.txt")

	if !fileExists(sampleFile) {
		sampleContent := fmt.Sprintf("# Placeholder for Day %s sample input - replace with actual sample data", dayPadded)
		if err := writeFile(sampleFile, sampleContent); err != nil {
			fmt.Printf("Error creating sample file: %v\n", err)
			os.Exit(1)
		}
		fmt.Printf("✅ Created sample: %s\n", sampleFile)
	}

	if !fileExists(inputFile) {
		inputContent := fmt.Sprintf("# Placeholder for Day %s real input - replace with actual puzzle input", dayPadded)
		if err := writeFile(inputFile, inputContent); err != nil {
			fmt.Printf("Error creating input file: %v\n", err)
			os.Exit(1)
		}
		fmt.Printf("✅ Created input: %s\n", inputFile)
	}

	// Print success message
	fmt.Printf("\n🎄 Day %s created successfully!\n", dayPadded)
	fmt.Println("📁 Files created:")
	fmt.Printf("  - %s\n", solutionFile)
	fmt.Printf("  - %s\n", testFile)
	fmt.Printf("  - %s\n", sampleFile)
	fmt.Printf("  - %s\n", inputFile)
	fmt.Println("\n📋 Next steps:")
	fmt.Printf("  1. Add your puzzle input to %s and %s\n", sampleFile, inputFile)
	fmt.Printf("  2. Implement the solution in %s\n", solutionFile)
	fmt.Printf("  3. Update test expectations in %s\n", testFile)
	fmt.Printf("  4. Run tests: dotnet test --filter \"%sTests\"\n", solutionClass)
}

func fileExists(filename string) bool {
	_, err := os.Stat(filename)
	return !os.IsNotExist(err)
}

func writeFile(filename, content string) error {
	// Ensure directory exists
	dir := filepath.Dir(filename)
	if err := os.MkdirAll(dir, 0755); err != nil {
		return err
	}

	return os.WriteFile(filename, []byte(strings.TrimSpace(content)), 0644)
}