import { isLoggedIn } from ".";
import { defaultState } from "store";

describe("isLoggedOn", () => {
	it("returns false if no token", () => {
		const state = defaultState;
		expect(state.auth.token).toBe(undefined);
		expect(isLoggedIn(state)).toBe(false);
	});

	it("returns false if token is falsy", () => {
		const state = {
			...defaultState,
			auth: { ...defaultState.auth, token: "" },
		};
		expect(isLoggedIn(state)).toBe(false);
	});

	it("returns true if token is truthy", () => {
		const state = {
			...defaultState,
			auth: { ...defaultState.auth, token: "token" },
		};
		expect(isLoggedIn(state)).toBe(true);
	});
});
