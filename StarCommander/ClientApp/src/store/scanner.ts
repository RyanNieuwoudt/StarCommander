import { Action, Reducer } from "redux";
import {
	all,
	cancel,
	delay,
	fork,
	put,
	take,
	takeEvery,
	takeLeading
} from "redux-saga/effects";
import { scan } from "client/ship";
import { querySaga } from "store/saga/templates";
import { SignIn, SignOut, SignUp } from "./auth";
import { OnCaptainBoarded } from "./ship";

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

function* scanner(shipId: string) {
	while (true) {
		yield put(actionCreators.scan(shipId));
		yield delay(3000);
	}
}

function* scanSaga(action: OnCaptainBoarded) {
	const scannerTask = yield fork(scanner, action.payload.shipId);
	yield take("SIGN_OUT");
	yield cancel(scannerTask);
}

export const rootSaga = function* root() {
	yield all([
		yield takeEvery("ON_CAPTAIN_BOARDED", scanSaga),
		yield takeLeading("SCAN", querySaga, scan)
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
			return action.data;
		default:
			return state;
	}
};
