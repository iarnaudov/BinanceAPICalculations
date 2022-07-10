## Description
This project gets latest data for BTCUSDT, ADAUSDT and ETHUSDT symbols from Binance Public API and stores it in databse.
Calculations over the data is later possible to be performed.

## Calculation Types:
-  Average price over the last 24 hours
- Simple moving average

## Run Instructions
Prerequisites:
- You should have Visual Studio installed (preferably version 2022).
- You must have actively running PostgreSQL server (version >14.0.0 is preferable).
- Replace your connection string before running the application. It is located in ForexDbContext file.
- Set up your start project (Forex for API and Forex.Console for Console APP)
- Click Start to run the appliacation

## Usage
### API
For API, you can check the swagger page, which will appear on start up.

Available endpoints:

* `GET /api/{symbol}/24hAvgPrice` - Returns the average price for the last 24h of data in the database ( or the oldest available, if 24h of data is not available )
     * `{symbol}` - The symbol the average price is being calculated for
* `GET /api/{symbol}/SimpleMovingAverage?n={numberOfDataPoints}&p={timePeriod}&s=[startDateTime]` - Return the current Simple Moving average of the symbol's price ( More info: [Investopedia](https://www.investopedia.com/terms/s/sma.asp#:~:text=A%20simple%20moving%20average%20(SMA)%20is%20an%20arithmetic%20moving%20average,periods%20in%20the%20calculation%20average.))
     * `{symbol}` - The symbol the average price is being calculated for
     * `n` - The amount of data points
     * `p` - The time period represented by each data point. Acceptable values: `1w`, `1d`, `30m`, `5m`, `1m`
     * `s` - The datetime from which to start the SMA calculation ( a date ) 
     
     
### Console Commands:

 * `24h {symbol}` - Same as the `/api/{symbol}/24hAvgPrice` endpoint
 * `sma {symbol} {n} {p} {s}` - Same as the `/api/{symbol}/SimpleMovingAverage` endpoint

