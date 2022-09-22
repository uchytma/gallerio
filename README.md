# 📸 Gallerio
Photo gallery management

## Features
- Manage multiple image galleries. One gallery can contains images from different directories.
- Browse images in galleries; which are sorted by capture date.
- Each image can be marked with multiple tags. Tags are stored in gallerio metadata file and in EXIF data of image.
- Images with specified tags can be exported (copied) to different location (API function only).

## Techstack
- BE: **.NET 6 (WebApi)**
- FE: **.NET 6 (Blazor)**
- External dependencies:
  - MetadataExtractor (https://github.com/drewnoakes/metadata-extractor-dotnet)
  - SixLabors.ImageSharp (https://github.com/SixLabors/ImageSharp)

## How to configure and run the project
- Clone repo
- Create database file at custom location. Database file structure is described bellow.
- Set Application.DatabasePath in user secrets (project Gallerio.Api) to database file.
- Run Gallerio.Api and Gallerio.Web projects.
- Manually call /api/gallery/{id}/multimediaSources/reindex API endpoint for each gallery.
- Now you can browse galleries in the web UI 🥳.

## Database file structure
```json
{
  "Galleries": [
    {
      "Id": "7858785c-e0a4-4a08-b112-0347754e478d", 
      "Name": "Norsko",
      "Description": "Norway roadtrip",
      "Date": "2022, Summer",
      "PhotosTotalCount": 0,
      "MultimediaSources": [
        {
          "Id": "7858785c-1234-4a08-b112-0347754e478d",
          "SourceDir": "Z:\\Photos\\2022\\Norway 2022"
        }
      ]
    }
  ]
}

```

-----

![gallerio-1](https://user-images.githubusercontent.com/28567403/190629190-7a9f1e7f-714e-49e0-b7a2-625de4277110.png)
