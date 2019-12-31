import { isLoggedOn } from ".";
import { ApplicationState } from "store";

describe("isLoggedOn", () => {
	it("returns false if no auth state", () => {
		const state: ApplicationState = {
			auth: undefined,
			counter: undefined,
			weatherForecasts: undefined
		};
		expect(isLoggedOn(state)).toBe(false);
	});

	it("returns false if no token", () => {
		const state: ApplicationState = {
			auth: {},
			counter: undefined,
			weatherForecasts: undefined
		};
		expect(isLoggedOn(state)).toBe(false);
	});

	it("returns false if token is falsy", () => {
		const state: ApplicationState = {
			auth: { token: "" },
			counter: undefined,
			weatherForecasts: undefined
		};
		expect(isLoggedOn(state)).toBe(false);
	});

	it("returns true if token is truthy", () => {
		const state: ApplicationState = {
			auth: { token: "token" },
			counter: undefined,
			weatherForecasts: undefined
		};
		expect(isLoggedOn(state)).toBe(true);
	});
});
