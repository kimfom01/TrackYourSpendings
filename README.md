# Budget

## Introduction
- This is an application where users can track their finances
- The application is tailored to simplify the often complex process of categorizing transactions and analyzing expenses, making personal finance management accessible to all users.

[//]: # (## Table of Contents)

[//]: # (- Provide a table of contents if the README is lengthy.)

---

## Installation

### Prerequisites
- Ensure [.NET SDK/Runtime](https://dotnet.microsoft.com/download) (version 7.0 is installed on your machine.
- Ensure you have `sql server` installed on your machine or you can connect to remote db.
- Install [Visual Studio](https://visualstudio.microsoft.com/) or your favorite editor/IDE.

### Getting the Project
- Clone the repository: `git clone https://github.com/kimfom01/TrackYourSpending.git`
- Alternatively, download and extract the project ZIP file.

### Configuration

[//]: # (- Set necessary environment variables in `.env` file or system environment.)
- Update `appsettings.json` with your typical sql server [connection string](https://www.connectionstrings.com/sql-server/).

### Building the Project
- Navigate to the project's root directory in the terminal.
- Run `dotnet build` to compile the project.

[//]: # (### Database Setup &#40;If Applicable&#41;)

[//]: # (- Run `dotnet ef database update` to apply migrations.)

### Running the Application
- Execute `dotnet run` within the project directory.
- Access the application via the provided local server URL for web projects.

### Publishing (For Deployment)
- Run `dotnet publish -c Release -o ./publish` to package the application for deployment.
- Deploy the contents of the `./publish` directory to your hosting environment.

---

## Features
- Ability to manage multiple wallets
- Group and filter transactions by categories and date
- Record transactions
- Reports, charts (upcoming feature)

---

[//]: # (## Contributing)

[//]: # (- Guidelines for those who want to contribute to the project.)

[//]: # (- Mention how they can submit pull requests and propose bug fixes or new features.)

[//]: # (## Code of Conduct)

[//]: # (- Outline expectations for participation and the process for reporting unacceptable behavior.)

[//]: # (## License)

[//]: # (- Specify the license under which the project is released.)

[//]: # (## Credits)

[//]: # (- Acknowledge contributors and any third-party resources or libraries used.)

[//]: # ()
[//]: # (## Contact Information)

[//]: # (- Provide contact details for further queries or discussions.)

[//]: # ()
[//]: # (## Changelog)

[//]: # (- &#40;Optional&#41; Include a changelog file detailing the chronological changes made to the project.)

[//]: # ()
[//]: # (## FAQs)

[//]: # (- &#40;Optional&#41; Frequently asked questions about the project.)

[//]: # ()
[//]: # (## Screenshots/Demo)

[//]: # (- &#40;Optional&#41; Include screenshots or a demo video to visually demonstrate your project.)

[//]: # ()
[//]: # (## Known Issues and Roadmap)

[//]: # (- &#40;Optional&#41; List any known issues and future plans for the project.)