import { Action, Reducer } from "redux";
import { all, takeLeading } from "redux-saga/effects";
import { setHeading, setSpeed } from "client/ship";
import { commandSaga } from "store/saga/templates";
import { SignIn, SignOut, SignUp } from "./auth";

export interface Position {
	x: number;
	y: number;
}

export interface ShipState {
	shipId?: string;
	position?: Position;
}

export interface OnCaptainBoarded {
	type: "ON_CAPTAIN_BOARDED";
	payload: { shipId: string };
}

export interface OnShipLocated {
	type: "ON_SHIP_LOCATED";
	payload: { position: Position };
}

export interface SetHeading {
	type: "SET_HEADING";
	payload: { shipId: string; heading: number };
}

export interface SetSpeed {
	type: "SET_SPEED";
	payload: { shipId: string; speed: number };
}

export type KnownAction =
	| OnCaptainBoarded
	| OnShipLocated
	| SignIn
	| SignOut
	| SignUp;

export const actionCreators = {
	setHeading: (shipId: string, heading: number) =>
		({
			type: "SET_HEADING",
			payload: {
				shipId,
				heading,
			},
		} as SetHeading),
	setSpeed: (shipId: string, speed: number) =>
		({
			type: "SET_SPEED",
			payload: {
				shipId,
				speed,
			},
		} as SetSpeed),
};

export const rootSaga = function* root() {
	yield all([
		yield takeLeading("SET_HEADING", commandSaga, setHeading),
		yield takeLeading("SET_SPEED", commandSaga, setSpeed),
	]);
};

const defaultState: ShipState = {};

export const reducer: Reducer<ShipState> = (
	state: ShipState = defaultState,
	incomingAction: Action
): ShipState => {
	const action = incomingAction as KnownAction;
	switch (action.type) {
		case "SIGN_IN":
		case "SIGN_OUT":
		case "SIGN_UP":
			return defaultState;
		case "ON_CAPTAIN_BOARDED":
			return action.payload;
		case "ON_SHIP_LOCATED":
			return { ...state, ...action.payload };
		default:
			return state;
	}
};
