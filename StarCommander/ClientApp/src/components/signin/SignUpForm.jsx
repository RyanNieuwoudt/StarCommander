import React, { useCallback } from "react";
import { useDispatch } from "react-redux";
import { Button, Form } from "grommet";
import { FormFieldLabel, RequiredFieldLabel } from "components/forms";
import { actionCreators } from "store/auth";

export default function SignUpForm() {
	const dispatch = useDispatch();

	const onSubmit = useCallback(
		(event) => {
			const { callSign, firstName, lastName, password } = event.value;
			dispatch(
				actionCreators.signUp(callSign, firstName, lastName, password)
			);
		},
		[dispatch]
	);

	return (
		<Form onSubmit={onSubmit}>
			<FormFieldLabel name="callSign" label="CallSign" required />
			<FormFieldLabel name="password" label="Password" required />
			<FormFieldLabel name="firstName" label="FirstName" required />
			<FormFieldLabel name="lastName" label="LastName" required />
			<Button type="submit" label="Sign up" primary />
			<RequiredFieldLabel />
		</Form>
	);
}
