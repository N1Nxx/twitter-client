[![Build Status](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2FN1Nxx%2Ftwitter-client%2Fbadge%3Fref%3Dmain&style=flat&label=build&logo=none)](https://actions-badge.atrox.dev/N1Nxx/twitter-client/goto?ref=main)

[About](https://stephane.dev/posts/creating-my-own-twitter-client/)

## Goal

Create a Twitter client that's fast and super customizable.

- [] Doesn't work with the default timeline
- [] Can load your lists, other users' lists
- [] Only loads what's configured (user/list)
- [] Can filter out retweets, replies, keyword (in or out) per user/list


## Development setup

#### Pre-commit hook

- Run the following commands to enable the pre-commit hook to run

```
find .git/hooks -type f -exec rm {} \;
find .githooks -type f -exec chmod +x {} \;
find .githooks -type f -exec ln -sf ../../{} .git/hooks/ \;
```
