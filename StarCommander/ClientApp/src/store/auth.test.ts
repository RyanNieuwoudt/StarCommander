import { reducer } from "./auth";

const expectedDefaultState = {};

const callSign = "callSign";
const firstName = "firstName";
const lastName = "lastName";
const password = "password";

const message = "message";
const token = "token";

describe("auth reducer", () => {
	it("should return the initial state", () => {
		expect(reducer(undefined, { type: "" })).toEqual(expectedDefaultState);
	});

	it("should sign in as requested", () => {
		expect(
			reducer(expectedDefaultState, {
				type: "SIGN_IN_SUCCESS",
				payload: { callSign, password },
				data: { player: { callSign, firstName, lastName }, token }
			})
		).toEqual({ player: { callSign, firstName, lastName }, token });
	});

	it("should sign up as requested", () => {
		expect(
			reducer(expectedDefaultState, {
				type: "SIGN_UP_SUCCESS",
				payload: { callSign, firstName, lastName },
				data: { player: { callSign, firstName, lastName }, token }
			})
		).toEqual({ player: { callSign, firstName, lastName }, token });
	});

	it("should clear state on sign out", () => {
		expect(
			reducer(
				{ player: { callSign, firstName, lastName }, token },
				{ type: "SIGN_OUT" }
			)
		).toEqual(expectedDefaultState);
	});

	it("should clear message when signing in", () => {
		expect(reducer({ message }, { type: "SIGN_IN" })).toEqual(
			expectedDefaultState
		);
	});

	it("should clear message when signing up", () => {
		expect(reducer({ message }, { type: "SIGN_UP" })).toEqual(
			expectedDefaultState
		);
	});

	it("should set message on sign in failure", () => {
		expect(
			reducer(expectedDefaultState, {
				type: "SIGN_IN_FAILURE",
				error: message
			})
		).toEqual({ message });
	});

	it("should set message on sign up failure", () => {
		expect(
			reducer(expectedDefaultState, {
				type: "SIGN_UP_FAILURE",
				error: message
			})
		).toEqual({ message });
	});
});
