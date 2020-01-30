import { Action, Reducer } from "redux";
import { all, takeEvery } from "redux-saga/effects";
import { scan } from "client/ship";
import { forwardSaga, querySaga } from "store/saga/templates";
import { SignIn, SignOut, SignUp } from "./auth";
import { OnShipLocated } from "./ship";

export interface Position {
	x: number;
	y: number;
}

export type ScannerState = Position[];

export interface Scan {
	type: "SCAN";
	payload: { shipId: string };
}

export interface ScanSuccess {
	type: "SCAN_SUCCESS";
	payload: { shipId: string };
	data: Position[];
}

export type KnownAction = ScanSuccess | SignIn | SignOut | SignUp;

export const actionCreators = {
	scan: (shipId: string) => ({ type: "SCAN", payload: { shipId } } as Scan)
};

export const rootSaga = function* root() {
	yield all([
		yield takeEvery("ON_SHIP_LOCATED", forwardSaga, "SCAN"),
		yield takeEvery("SCAN", querySaga, scan)
	]);
};

const defaultState: ScannerState = [];

export const reducer: Reducer<ScannerState> = (
	state: ScannerState = defaultState,
	incomingAction: Action
): ScannerState => {
	const action = incomingAction as KnownAction;
	switch (action.type) {
		case "SIGN_IN":
		case "SIGN_OUT":
		case "SIGN_UP":
			return defaultState;
		case "SCAN_SUCCESS":
			console.log(action);
			return action.data;
		default:
			return state;
	}
};
