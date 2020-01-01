import * as auth from "./auth";

export interface ApplicationState {
	auth: auth.AuthState | undefined;
}

export const reducers = {
	auth: auth.reducer
};

export const sagas = {
	auth: auth.rootSaga
};

export interface Action {
	type: string;
	payload: object | undefined;
}

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
	(
		dispatch: (action: TAction) => void,
		getState: () => ApplicationState
	): void;
}
