# Words Guesser

[![MIT license](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)](LICENSE)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

[![Open Source Love svg1](https://badges.frapsoft.com/os/v1/open-source.svg?v=103)](https://github.com/ellerbrock/open-source-badges/)
[![Run Super-Linter](https://github.com/JakubKorytko/words-guesser/actions/workflows/super-linter.yml/badge.svg)](https://github.com/JakubKorytko/words-guesser/actions/workflows/super-linter.yml)

## Table of Contents

- [Words Guesser](#words-guesser)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Words file](#words-file)
  - [Usage](#usage)
  - [Contributing](#contributing)
  - [Contact](#contact)
  - [License](#license)

## Introduction

Words Guesser is a .NET console application written in C# and built using the Visual Studio 2022,
that allows you to play a word guessing game.
It combines simplicity with readability, maintainability and extensibility,
and has a user-friendly interface that is really easy to use.
You can also easily add your own words to the game (see [words file](#words-file) section).

This makes it a great, easy to add tool to include in your application
as a fun little game for your users.
For example, when the user is waiting for something to load.

**This project was created targeting Windows. It has not been tested on other operating systems.**

## Prerequisites

Before you use the Words Guesser, make sure that you have the following prerequisites:

- [Visual Studio 2022 (version 17.6.3 or later)](https://visualstudio.microsoft.com/vs/)
  - ".NET desktop development" workload installed.

## Installation

1. Clone this repository to your local machine using Git,
or download the ZIP file and extract it to a directory of your choice:

    ```bash
    git clone https://github.com/JakubKorytko/words-guesser.git
    ```

1. Change to the project directory:

    ```bash
    cd words-guesser
    ```

1. Open the [WordsGuesser.sln](./WordsGuesser.sln) solution file in Visual Studio.

1. Install the NuGet packages:
    - Automatically:
      - Make sure that in the `Tools -> Options -> NuGet Package Manager -> General` (toolbar) the following options are selected:

        - [x] Automatically check for missing packages during build in Visual Studio
        - [x] Allow NuGet to download missing packages during build

        If they are not, select them and click `OK`
    - Manually:
      - Select `Tools -> NuGet Package Manager -> Package Manager Console` from the toolbar
      - In the console, type the following command and press `Enter`:

        ```bash
        Update-Package
        ```

1. Rename [words.example.json](./words.example.json) to `words.json` or provide your own [words file](#words-file).

1. Build or run the project in Visual Studio:

    - **Run the project** in the Visual Studio
      - `F5` /  `Ctrl + F5` by default
      - or `Debug` -> `Start Debugging` / `Start Without Debugging` on the toolbar

    - or **Build project** executable
      - `Ctrl + B` / `Ctrl + Shift + B` by default
      - or `Build` -> `Build Solution` / `Build WordsGuesser` on the toolbar

    The executable will be available in the `build` directory in both cases.

## Words file

For the program to work, you need to provide a `words.json` file in the root directory of the project.

The file should contain a JSON array of objects (categories) with the following properties:

- `categoryName` - the name of the category (e.g. `animals`)
- `words` - an array of words in the category (e.g. `["cat", "dog", "mouse"]`)

For example:

```json
[
    // (...),
    {
        "categoryName": "currencies",
        "words": [
            "yuan",
            "euro",
            "dollar",
            "yen",
            "pound"
        ]
    },
    // (...)
]
```

There is a [words.example.json](./words.example.json) file in the repository.
You can rename it to `words.json` and use it as a template.

## Usage

Using the application is straightforward.
It doesn't use any menus (except for the choice to play again or exit the app after the game) or command-line arguments.

When you run the app, it loads the `words.json` file and starts the game automatically.

You can now guess the letters of the word one by one.
If you guess a letter correctly, it will be revealed in the word.
If you guess incorrectly, you will lose a turn.

You have limited number of tries, so be careful!
It is determined by the `private const int GuessesLimit` field in the `Game` class
([Components/Game/GameLogic.cs](./Components/Game/GameLogic.cs))

The game ends when you guess the word correctly or when you run out of tries.

After the game ends, you can choose to play again or exit the application.

## Contributing

If you find issues or have suggestions for improvements,
feel free to open an issue or submit a pull request.
Contributions are welcome!

## Contact

If you have any questions, feel free to contact me at <jakub@korytko.me>.

## License

This project is released under the MIT License. See the [LICENSE](LICENSE) file for details.
