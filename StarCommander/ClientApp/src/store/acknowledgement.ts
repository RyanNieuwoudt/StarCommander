import * as R from "ramda";
import { Reducer } from "redux";
import { SignIn, SignOut, SignUp } from "./auth";

export type AcknowledgementState = string[];

export interface OnCaptainBoarded {
	type: "ON_CAPTAIN_BOARDED";
	payload: { shipId: string };
}

export interface SetHeadingSuccess {
	type: "SET_HEADING_SUCCESS";
	payload: { shipId: string; heading: number };
}

export interface SetSpeedSuccess {
	type: "SET_SPEED_SUCCESS";
	payload: { shipId: string; speed: number };
}

export type KnownAction =
	| OnCaptainBoarded
	| SetHeadingSuccess
	| SetSpeedSuccess
	| SignIn
	| SignOut
	| SignUp;

export const rootSaga = function* root(): any {};

export const defaultState: AcknowledgementState = [];

export const reducer: Reducer<AcknowledgementState> = (
	state: AcknowledgementState = defaultState,
	action: KnownAction
): AcknowledgementState => {
	switch (action.type) {
		case "SIGN_IN":
		case "SIGN_OUT":
		case "SIGN_UP":
			return defaultState;
		case "ON_CAPTAIN_BOARDED":
			return R.append("Welcome aboard, captain.", state);
		case "SET_HEADING_SUCCESS":
			return R.append(`Heading set to ${action.payload.heading}.`, state);
		case "SET_SPEED_SUCCESS":
			return R.append(`Speed set to ${action.payload.speed}.`, state);
		default:
			return state;
	}
};
