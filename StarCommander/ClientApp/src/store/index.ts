import * as auth from "./auth";
import * as WeatherForecasts from "./WeatherForecasts";
import * as Counter from "./Counter";

export interface ApplicationState {
	auth: auth.AuthState | undefined;
	counter: Counter.CounterState | undefined;
	weatherForecasts: WeatherForecasts.WeatherForecastsState | undefined;
}

export const reducers = {
	auth: auth.reducer,
	counter: Counter.reducer,
	weatherForecasts: WeatherForecasts.reducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
	(
		dispatch: (action: TAction) => void,
		getState: () => ApplicationState
	): void;
}
