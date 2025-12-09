package main

import (
	"fmt"
	"os"
	"os/exec"
	"strconv"
)

func main() {
	if len(os.Args) < 2 {
		fmt.Println("Usage: go run new-day.go <day-number>")
		fmt.Println("Example: go run new-day.go 2")
		os.Exit(1)
	}

	dayArg := os.Args[1]

	// Validate day number
	dayNum, err := strconv.Atoi(dayArg)
	if err != nil || dayNum < 1 || dayNum > 25 {
		fmt.Println("Error: Please provide a valid day number (1-25)")
		os.Exit(1)
	}

	// Run lein command
	cmd := exec.Command("lein", "run", "-m", "aoc2025.new-day", dayArg)
	cmd.Stdout = os.Stdout
	cmd.Stderr = os.Stderr

	if err := cmd.Run(); err != nil {
		fmt.Printf("Error running command: %v\n", err)
		os.Exit(1)
	}
}
