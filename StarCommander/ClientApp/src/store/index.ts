import * as acknowledgement from "./acknowledgement";
import * as auth from "./auth";
import * as scanner from "./scanner";
import * as ship from "./ship";

export interface ApplicationState {
	acknowledgement: acknowledgement.AcknowledgementState;
	auth: auth.AuthState;
	scanner: scanner.ScannerState;
	ship: ship.ShipState;
}

export const defaultState = {
	acknowledgement: acknowledgement.defaultState,
	auth: auth.defaultState,
	scanner: scanner.defaultState,
	ship: ship.defaultState,
};

export const reducers = {
	acknowledgement: acknowledgement.reducer,
	auth: auth.reducer,
	scanner: scanner.reducer,
	ship: ship.reducer,
};

export const sagas = {
	acknowledgement: acknowledgement.rootSaga,
	auth: auth.rootSaga,
	scanner: scanner.rootSaga,
	ship: ship.rootSaga,
};

export interface Action {
	type: string;
	payload: Record<string, unknown> | undefined;
}

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
	(
		dispatch: (action: TAction) => void,
		getState: () => ApplicationState
	): void;
}
