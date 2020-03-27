import React, { useCallback } from "react";
import { useDispatch } from "react-redux";
import { Button, Form } from "grommet";
import { FormFieldLabel, RequiredFieldLabel } from "components/forms";
import { actionCreators } from "store/auth";

export default function SignInForm() {
	const dispatch = useDispatch();

	const onSubmit = useCallback(
		(event) => {
			const { callSign, password } = event.value;
			dispatch(actionCreators.signIn(callSign, password));
		},
		[dispatch]
	);

	return (
		<Form onSubmit={onSubmit}>
			<FormFieldLabel name="callSign" label="CallSign" required />
			<FormFieldLabel name="password" label="Password" required />
			<Button type="submit" label="Sign in" primary />
			<RequiredFieldLabel />
		</Form>
	);
}
