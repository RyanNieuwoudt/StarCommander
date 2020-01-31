import { isLoggedIn } from ".";
import { ApplicationState } from "store";

describe("isLoggedOn", () => {
	it("returns false if no auth state", () => {
		const state: ApplicationState = { auth: undefined, ship: {} };
		expect(isLoggedIn(state)).toBe(false);
	});

	it("returns false if no token", () => {
		const state: ApplicationState = { auth: {}, ship: {} };
		expect(isLoggedIn(state)).toBe(false);
	});

	it("returns false if token is falsy", () => {
		const state: ApplicationState = { auth: { token: "" }, ship: {} };
		expect(isLoggedIn(state)).toBe(false);
	});

	it("returns true if token is truthy", () => {
		const state: ApplicationState = { auth: { token: "token" }, ship: {} };
		expect(isLoggedIn(state)).toBe(true);
	});
});
