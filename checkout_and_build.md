# Cromium checkout and build

[googlesource.com-Anleitung](https://chromium.googlesource.com/chromium/src/+/master/docs/windows_build_instructions.md)

## Download depot_tools

[download depot_tools](https://storage.googleapis.com/chrome-infra/depot_tools.zip)

## Environment variables

| env                       | value                                 |
| ------------------------- | ------------------------------------- |
| PATH                      | [add unpacked depot_tools path to it] |
| DEPOT_TOOLS_WIN_TOOLCHAIN | 0                                     |
| GYP_MSVS_VERSION          | 2019                                  |

## cmd commands

````cmd
gclient

mkdir chromium && cd chromium
fetch --no-history chromium

cd src
gn gen --ide=vs out\Default

devenv out\Default\all.sln
````
