import { reducer } from "./auth";

const expectedDefaultState = {};

describe("auth reducer", () => {
	it("should return the initial state", () => {
		expect(reducer(undefined, { type: "" })).toEqual(expectedDefaultState);
	});

	it("should sign up as requested", () => {
		const callSign = "callSign";
		const firstName = "firstName";
		const lastName = "lastName";

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
});
