import { reducer } from "./auth";

const emptyState = {};

const callSign = "callSign";
const firstName = "firstName";
const lastName = "lastName";
const password = "password";

const message = "message";
const token = "token";

describe("auth reducer", () => {
	it("should return the initial state", () => {
		expect(reducer(undefined, { type: "" })).toEqual(emptyState);
	});

	it("should sign in as requested", () => {
		expect(
			reducer(emptyState, {
				type: "SIGN_IN_SUCCESS",
				payload: { callSign, password },
				data: { player: { callSign, firstName, lastName }, token }
			})
		).toEqual({ player: { callSign, firstName, lastName }, token });
	});

	it("should sign up as requested", () => {
		expect(
			reducer(emptyState, {
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
		).toEqual(emptyState);
	});

	it("should clear message when signing in", () => {
		expect(reducer({ message }, { type: "SIGN_IN" })).toEqual(emptyState);
	});

	it("should clear message when signing up", () => {
		expect(reducer({ message }, { type: "SIGN_UP" })).toEqual(emptyState);
	});

	it("should set message on sign in failure", () => {
		expect(
			reducer(emptyState, {
				type: "SIGN_IN_FAILURE",
				error: message
			})
		).toEqual({ message });
	});

	it("should set message on sign up failure", () => {
		expect(
			reducer(emptyState, {
				type: "SIGN_UP_FAILURE",
				error: message
			})
		).toEqual({ message });
	});

	it("should update name on success, preserving callSign", () => {
		expect(
			reducer(
				{ player: { callSign } },
				{
					type: "UPDATE_NAME_SUCCESS",
					payload: { firstName, lastName }
				}
			)
		).toEqual({ player: { callSign, firstName, lastName } });
	});

	it("should update name on notification, preserving callSign", () => {
		expect(
			reducer(
				{ player: { callSign } },
				{
					type: "ON_PLAYER_NAME_UPDATED",
					payload: { firstName, lastName }
				}
			)
		).toEqual({ player: { callSign, firstName, lastName } });
	});
});
