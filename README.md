# Hacker News API

The Hacker News API is a service that provides information about the best stories from Hacker News, sorted by their score.

## Table of Contents

- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
- [Contributing](#contributing)

## Getting Started

### Prerequisites

- .NET Core SDK [Install .NET Core](https://dotnet.microsoft.com/download)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/HackerNewsAPI.git
   cd HackerNewsAPI
    ```
Build the project:
```sh
dotnet build
```
Run the application:
```sh
dotnet run
```

The API will be available at https://localhost:5001.

## Configuration
The configuration of the API is defined in appsettings.json. You can customize the settings according to your needs.

## Usage
You can access the following endpoints to interact with the API:

`GET /beststories/123`: Retrieve a list of the best stories from Hacker News.

## API Endpoints
`GET /beststories`
Retrieve a list of the best stories from Hacker News, sorted by score.

Response:

```
[
  {
    "title": "A uBlock Origin update was rejected from the Chrome Web Store",
    "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
    "postedBy": "ismaildonmez",
    "time": "2019-10-12T13:43:01+00:00",
    "score": 1716,
    "commentCount": 572
  },
  ...
]
```

## Testing
TBD
Add unit tests + coverage + GithubAction on PR

## Contributing
TBD
