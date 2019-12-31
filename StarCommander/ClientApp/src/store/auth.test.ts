import { reducer } from "./auth";

const expectedDefaultState = {};

const callSign = "callSign";
const firstName = "firstName";
const lastName = "lastName";

describe("auth reducer", () => {
	it("should return the initial state", () => {
		expect(reducer(undefined, { type: "" })).toEqual(expectedDefaultState);
	});

	it("should sign up as requested", () => {
		expect(
			reducer(expectedDefaultState, {
				type: "SIGN_UP",
				payload: {
					callSign,
					firstName,
					lastName
				}
			})
		).toEqual({ callSign, firstName, lastName, token: "token" });
	});

	it("should clear state on sign out", () => {
		expect(
			reducer({ callSign, firstName, lastName }, { type: "SIGN_OUT" })
		).toEqual(expectedDefaultState);
	});
});
