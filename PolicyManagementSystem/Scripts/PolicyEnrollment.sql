CREATE TABLE policy_enrollment (
    id SERIAL PRIMARY KEY,

    policy_id INTEGER NOT NULL,
    user_id INTEGER NOT NULL,

    status VARCHAR(50) NOT NULL,

    requested_at TIMESTAMP,
    approved_at TIMESTAMP,

    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_policy_enrollment_policy
        FOREIGN KEY (policy_id)
        REFERENCES policy(id)
        ON DELETE CASCADE,

    CONSTRAINT fk_policy_enrollment_user
        FOREIGN KEY (user_id)
        REFERENCES users(id)
        ON DELETE CASCADE
);